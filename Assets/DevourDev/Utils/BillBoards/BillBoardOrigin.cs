using UnityEngine;

namespace DevourDev.Unity.Utils.BillBoards
{
    public class BillBoardOrigin : MonoBehaviour
    {
        private Vector3 _lastRotEulers;


        public Vector3 LastRotationEulers => _lastRotEulers;


        public event System.Action<Vector3> OnRotation;


        private void Awake()
        {
            _lastRotEulers = transform.rotation.eulerAngles;
        }


        public void Sync()
        {
            _lastRotEulers = transform.rotation.eulerAngles;
            OnRotation?.Invoke(transform.rotation.eulerAngles);
        }
    }
}
