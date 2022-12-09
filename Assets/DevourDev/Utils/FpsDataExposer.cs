#define ENABLE_ARRAY_POOLING

using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class FpsDataExposer : MonoBehaviour
    {
        [SerializeField] private float _fps;
        [SerializeField] private float _frameTime;
        [SerializeField] private bool _offsetDetected;

        [SerializeField] private FpsManager _fpsManager;


        private void Start()
        {
            _fpsManager.OnDataUpdated += HandleDataUpdated;
        }

        private void HandleDataUpdated(float fps, bool offsetDetected)
        {
            _fps = fps;
            _frameTime = 1f / fps;
            _offsetDetected = offsetDetected;
        }
    }
}