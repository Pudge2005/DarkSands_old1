using System;

namespace Game.Core.Abilities
{
    public interface IAbilityLifeHandle
    {
        event Action OnCancelled;


        void Cancel();
    }



    //public sealed class AbilitiesManagerInitializer : MonoBehaviour
    //{
            //когда создавал статику, сразу создал этот монобех, но уже не помню, для чего
    //}
}
