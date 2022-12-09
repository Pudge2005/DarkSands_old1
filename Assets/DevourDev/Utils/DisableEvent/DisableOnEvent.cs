using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public class DisableOnEvent : MonoBehaviour
    {
        [SerializeField] private DisableEventSource _source;
        [SerializeField] private Behaviour[] _behavioursToDisable;


        private void OnEnable()
        {
            _source.OnCommand += HandleCommand;
        }

        private void OnDisable()
        {
            _source.OnCommand -= HandleCommand;
        }


        private void HandleCommand(bool enabled)
        {
            foreach (var b in _behavioursToDisable)
            {
                //todo: check if OnEnable() calls
                //if already enabled behaviour
                //sets to enable = true

                if (b.enabled != enabled)
                    b.enabled = enabled;
            }
        }
    }
}
