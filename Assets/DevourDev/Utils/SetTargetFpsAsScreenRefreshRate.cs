using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public class SetTargetFpsAsScreenRefreshRate : MonoBehaviour
    {
        [SerializeField] private FpsManager _fpsManager;


        private void Start()
        {
            _fpsManager.TargetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}