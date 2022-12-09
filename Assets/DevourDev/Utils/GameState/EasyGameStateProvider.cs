using System;

namespace DevourDev.Unity.Utils.GameState
{
    public static class EasyGameStateProvider
    {
        private static bool _isPaused;
        private static bool _isPlayerAlive;

        public static bool IsPaused
        {
            get => _isPaused;

            internal set
            {
                if (_isPaused == value)
                    return;

                _isPaused = value;
                OnPauseStateChanged?.Invoke(value);
            }
        }

        public static bool IsPlayerAlive
        {
            get => _isPlayerAlive;

            internal set
            {
                if (_isPlayerAlive == value)
                    return;

                _isPlayerAlive = value;
                OnPlayerAliveStateChanged?.Invoke(value);
            }
        }


        public static event Action<bool> OnPauseStateChanged;
        public static event Action<bool> OnPlayerAliveStateChanged;
    }
}
