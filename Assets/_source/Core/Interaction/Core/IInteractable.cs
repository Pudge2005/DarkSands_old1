namespace Game.Core.Interaction
{
    public interface IInteractable<TInteractor>
    {
        void Interact(TInteractor interactor);
    }
}
