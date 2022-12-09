using UnityEngine;

namespace DevourDev.Unity.Utils.BillBoards
{
    [RequireComponent(typeof(BillBoard))]
    public class BillBoardToMainCameraLinker : MonoBehaviour
    {
        private static BillBoardOrigin _mainCamOrigin;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetMainCamOrigin()
        {
            _mainCamOrigin = null;
        }


        private void Start()
        {
            if (_mainCamOrigin == null)
            {
                //unable to private static readonly
                //BillBoardOrigin = Camera.main...
                //due to MainThread limitations

                _mainCamOrigin = Camera.main.gameObject.GetComponent<BillBoardOrigin>();
            }

            GetComponent<BillBoard>().Origin = _mainCamOrigin;
            Destroy(this);
        }
    }
}
