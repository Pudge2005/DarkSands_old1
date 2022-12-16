using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Core.Interaction
{
    public abstract class RaiseEventOnInteractionComponent<TInteractor> : MonoBehaviour, IInteractable<TInteractor>
    {
        [SerializeField] private UnityEvent _unityEvent;
        [SerializeField] private UnityEvent<TInteractor> _unityEventWithParameter;


        public event System.Action<RaiseEventOnInteractionComponent<TInteractor>, TInteractor> OnInteraction;


        public void Interact(TInteractor interactor)
        {
            OnInteraction?.Invoke(this, interactor);
            _unityEvent?.Invoke();
            _unityEventWithParameter?.Invoke(interactor);
        }
    }
}
