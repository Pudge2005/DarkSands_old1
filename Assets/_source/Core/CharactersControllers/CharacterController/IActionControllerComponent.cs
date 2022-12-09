namespace Game.Core.CharactersControllers
{
    public interface IActionControllerComponent
    {
        void Trigger();
    }


    public interface IActionControllerComponent<TContext> where TContext : struct
    {
        TContext InputValue { get; set; }
    }
}