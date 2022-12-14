using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public sealed class Updator : MonoBehaviour
    {
        private static Updator _inst;


        public event System.Action OnUpdate;
        public event System.Action OnFixedUpdate;
        public event System.Action OnLateUpdate;


        public static Updator Shared
        {
            get
            {
                if (_inst == null)
                    _inst = new GameObject("[Updator]").AddComponent<Updator>();

                return _inst;
            }
        }


        private void Awake()
        {
            _inst = this;
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}
