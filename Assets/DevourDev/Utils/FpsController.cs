using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public class FpsController : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField] private FpsManager _fpsManager;

#if UNITY_EDITOR
        private int _tmp;
#endif


        private void Awake()
        {
            UpdateTargetFrameRate(_targetFrameRate);
#if UNITY_EDITOR
            _tmp = _targetFrameRate;
#endif
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (_tmp != _targetFrameRate)
            {
                _tmp = _targetFrameRate;
                UpdateTargetFrameRate(_targetFrameRate);
            }
        }
#endif

        private void UpdateTargetFrameRate(int targetFrameRate)
        {
            if (_fpsManager == null)
                Application.targetFrameRate = targetFrameRate;
            else
                _fpsManager.TargetFrameRate = targetFrameRate;
        }
    }
}