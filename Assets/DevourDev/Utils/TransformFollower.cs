using UnityEngine;

namespace DevourDev.Unity.Utils
{

    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private bool _x = true;
        [SerializeField] private bool _y = true;
        [SerializeField] private bool _z = true;

        [SerializeField] private Vector3 _offset;
#if UNITY_EDITOR
        [SerializeField] private bool _calcOffset;
#endif



        private Vector3 _lastOriginPos;
        private Transform _tr;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_calcOffset)
            {
                _calcOffset = false;
                _offset = transform.position - _origin.position;
            }
        }
#endif

        private void Start()
        {
            _tr = transform;
            WarmUp();
        }

        private void WarmUp()
        {
            _lastOriginPos = _origin.transform.position;
            Sync();
        }

        private void Sync()
        {
            Vector3 oldPos = _tr.position;
            Vector3 originPos = _lastOriginPos + _offset;
            Vector3 newPos = oldPos;

            if (_x)
                newPos.x = originPos.x;

            if (_y)
                newPos.y = originPos.y;

            if (_z)
                newPos.z = originPos.z;

            _tr.position = newPos;
        }

        public void CheckForSync()
        {
            if (CheckEquality(out var orig))
                return;

            _lastOriginPos = orig;
            Sync();
        }


        private bool CheckEquality(out Vector3 orig)
        {
            orig = _origin.position;
            Vector3 last = _lastOriginPos;

            return (!_x || orig.x == last.x)
                && (!_y || orig.y == last.y)
                && (!_z || orig.z == last.z);
        }
    }
}
