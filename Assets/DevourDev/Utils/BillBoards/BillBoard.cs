using DevourDev.Unity.Utils.Callbacks;
using UnityEngine;

namespace DevourDev.Unity.Utils.BillBoards
{
    public class BillBoard : MonoBehaviour
    {
        [SerializeField] private VisibilityCallbackable _visibility;
        [SerializeField] private BillBoardOrigin _origin;
        [SerializeField] private bool _syncX;
        [SerializeField] private bool _syncY;
        [SerializeField] private bool _syncZ;

        public BillBoardOrigin Origin { get => _origin; set => SetOrigin(value); }


        private void Awake()
        {
            _visibility.OnVisibleStateChanged += HandleVisibleStateChanged;
        }


        private void HandleVisibleStateChanged(bool visible)
        {
            if (_origin == null)
                return;

            if (visible)
            {
                _origin.OnRotation += HandleRotation;
                HandleRotation(_origin.LastRotationEulers);
            }
            else
            {
                _origin.OnRotation -= HandleRotation;
            }
        }


        private void SetOrigin(BillBoardOrigin origin)
        {
            if (_origin != null && _visibility.Visible)
                _origin.OnRotation -= HandleRotation;

            _origin = origin;

            if (_origin != null && _visibility.Visible)
                _origin.OnRotation += HandleRotation;
        }


        private void OnDestroy()
        {
            SetOrigin(null);
        }


        private void HandleRotation(Vector3 eulers)
        {
            //без проверки на одинаковый ротейшн, так
            //как это очень маловероятно + уже будет
            //проведена дорогая конверция Quaternion->Eulers

            var oldRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion
                .Euler(_syncX ? eulers.x : oldRot.x,
                       _syncY ? eulers.y : oldRot.y,
                       _syncZ ? eulers.z : oldRot.z);
        }
    }
}
