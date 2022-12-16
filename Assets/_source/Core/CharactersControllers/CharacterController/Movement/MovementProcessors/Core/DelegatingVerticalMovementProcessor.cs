using System;

namespace Game.Core.CharactersControllers
{
    public sealed class DelegatingVerticalMovementProcessor : IVerticalMovementProcessor
    {
        private readonly Func<float, float> _processVerticalVelocityAction;


        public DelegatingVerticalMovementProcessor(Func<float, float> processVerticalVelocityAction)
        {
            _processVerticalVelocityAction = processVerticalVelocityAction;
        }
            

        public float ProcessVerticalVelocity(float vertical)
        {
            return _processVerticalVelocityAction(vertical);
        }
    }
}
