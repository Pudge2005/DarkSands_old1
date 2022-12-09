using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    //public sealed class FastAccess : MonoBehaviour
    //{
    //    //prototype-only class

    //}
    public abstract class AttackAbility : AbilitySo
    {
        [SerializeField] private float _damage;
        [SerializeField] private GameObject _instantiateOnHit;


        protected void DealDamage(Character target)
        {
            if(_instantiateOnHit != null)
            {
                Instantiate(_instantiateOnHit, target.transform.position, Quaternion.identity);
            }

            target.DealDamage(_damage);
        }
    }
}
