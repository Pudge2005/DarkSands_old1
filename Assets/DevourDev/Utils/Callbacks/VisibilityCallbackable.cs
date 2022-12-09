using UnityEngine;

namespace DevourDev.Unity.Utils.Callbacks
{
    public class VisibilityCallbackable : MonoBehaviour
    {
        public bool Visible { get; private set; }


        public event System.Action<bool> OnVisibleStateChanged;


        private void OnBecameVisible()
        {
            if (Visible)
                return;

            Visible = true;
            OnVisibleStateChanged?.Invoke(Visible);
        }

        private void OnBecameInvisible()
        {
            if (!Visible)
                return;

            Visible = false;
            OnVisibleStateChanged?.Invoke(Visible);
        }
    }
}
