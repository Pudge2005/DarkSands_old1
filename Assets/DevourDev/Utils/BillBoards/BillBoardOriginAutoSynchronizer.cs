using UnityEngine;

namespace DevourDev.Unity.Utils.BillBoards
{
    [RequireComponent(typeof(BillBoardOrigin))]
    public class BillBoardOriginAutoSynchronizer : MonoBehaviour
    {
        private abstract class SyncerBase : MonoBehaviour
        {
            private BillBoardOrigin _origin;
            private Quaternion _prevRot;


            public void Init(BillBoardOrigin origin)
            {
                _origin = origin;
                _prevRot = transform.rotation;
            }

            protected void Sync()
            {
                var rot = transform.rotation;

                if (rot == _prevRot)
                    return;

                _prevRot = rot;
                _origin.Sync();
            }
        }


        private class EveryFrameSyncer : SyncerBase
        {
            private void Update()
            {
                Sync();
            }
        }


        private class FixedUpdateSyncer : SyncerBase
        {
            private void FixedUpdate()
            {
                Sync();
            }
        }


        private class SecondsDeltaSyncer : SyncerBase
        {
            private float _sd;
            private float _sdLeft;


            public void SetSecondsDelta(float sd)
            {
                _sd = sd;
                ResetCD();
            }

            private void ResetCD()
            {
                _sdLeft = _sd;
            }


            private void Update()
            {
                if ((_sdLeft -= Time.deltaTime) > 0)
                    return;

                ResetCD();
                Sync();
            }
        }


        private class FramesDeltaSyncer : SyncerBase
        {
            private int _fd; //frames delta
            private int _fLeft; //frames left


            public void SetFramesDelta(int fd)
            {
                _fd = fd;
                ResetCD();
            }

            private void ResetCD() //count down
            {
                _fLeft = _fd;
            }


            private void Update()
            {
                if ((--_fLeft) > 0)
                    return;

                ResetCD();
                Sync();
            }
        }


        [SerializeField] private SyncMode _checkFrequency = SyncMode.EveryFrame;
        [SerializeField] private float _delta;


        private void Awake()
        {
            var origin = gameObject.GetComponent<BillBoardOrigin>();
            SyncerBase sbase;

            switch (_checkFrequency)
            {
                case SyncMode.EveryFrame:
                    sbase = gameObject.AddComponent<EveryFrameSyncer>();
                    break;
                case SyncMode.SecondsDelta:
                    var secondsSyncer = gameObject.AddComponent<SecondsDeltaSyncer>();
                    secondsSyncer.SetSecondsDelta(_delta);
                    sbase = secondsSyncer;
                    break;
                case SyncMode.FramesDelta:
                    var framesSyncer = gameObject.AddComponent<FramesDeltaSyncer>();
                    framesSyncer.SetFramesDelta((int)_delta);
                    sbase = framesSyncer;
                    break;
                case SyncMode.FixedUpdate:
                    sbase = gameObject.AddComponent<FixedUpdateSyncer>();
                    break;
                default:
                    throw new System.InvalidOperationException($"Unexpected enum value: {_checkFrequency}");
            }

            sbase.Init(origin);
            Destroy(this);
        }
    }
}
