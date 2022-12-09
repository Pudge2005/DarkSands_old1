using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilitySo : SoDatabaseElement, IAbility
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private DynamicStatAmount[] _cost;
        [SerializeField] private ConstantStatAmount[] _requirements;
        //[SerializeField] private float _castTime;


        public void Cast(Character caster)
        {
            Debug.Log($"Character {caster.gameObject.name} used ability {name}");
            CastInherited(caster);
        }


        //todo: rename
        protected abstract void CastInherited(Character caster);
    }
}
