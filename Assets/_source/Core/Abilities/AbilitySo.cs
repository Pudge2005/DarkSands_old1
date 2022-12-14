using System;
using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using Game.Core.Characters;
using Game.Core.GameRules;
using UnityEngine;

namespace Game.Core.Abilities
{

    public abstract class AbilitySo : SoDatabaseElement, IAbility
    {
        [System.Serializable]
        internal struct AbilityStage
        {
            [SerializeField] private CastStageSo _stage;
            [SerializeField] private float _time;


            public CastStageSo Stage => _stage;
            public float Time => _time;
        }


        [SerializeField] private MetaInfo _metaInfo;

        [SerializeField] private DynamicStatAmount[] _cost;
        [SerializeField] private ConstantStatAmount[] _requirements;

        [SerializeField] private AbilityStage[] _stages;
#if UNITY_EDITOR
        [SerializeField] private bool _processStages;
#endif


        internal AbilityStage[] Stages => _stages;



#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_processStages)
            {
                _processStages = false;
                ProcessStages();
            }
        }


        private void ProcessStages()
        {
            Array.Sort(_stages, (l, r) => l.Stage.Order.CompareTo(r.Stage.Order));
            UnityEditor.EditorUtility.SetDirty(this);

            //var dict = ActiveGameRules.CastStagesSettingsEditor.Order;
            //Array.Sort(_stages, (l, r) => dict[l.Stage].CompareTo(dict[r.Stage]));
        }
#endif

        public bool Cast(Character caster, out IAbilityLifeHandle handle)
        {
            Debug.Log($"Character {caster.gameObject.name} attempts to use ability {name}");

            if (!CanCast(caster))
            {
                Debug.Log($"Character {caster.gameObject.name} can not use ability {name} now");
                handle = default!;
                return false;
            }

            Debug.Log($"Character {caster.gameObject.name} used ability {name}");

            handle = AbilitiesManager.Cast(this, caster);
            return true;
        }


        internal void StartCastStage(Character caster, CastStageSo stage)
        {
            HandleCastStageStarted(caster, stage);
        }

        internal void EndCastStage(Character caster, CastStageSo stage)
        {
            HandleCastStageEnded(caster, stage);
        }


        protected abstract bool CanCast(Character caster);
        protected abstract void HandleCastStageStarted(Character caster, CastStageSo stage);
        protected abstract void HandleCastStageEnded(Character caster, CastStageSo stage);
    }



    //public sealed class AbilitiesManagerInitializer : MonoBehaviour
    //{
            //когда создавал статику, сразу создал этот монобех, но уже не помню, для чего
    //}
}
