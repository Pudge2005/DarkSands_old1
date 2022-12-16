using System;
using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using Game.Core.Characters;
using Game.Core.GameRules;
using Game.Core.Stats;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilitySo<TContext> : SoDatabaseElement, IAbility<TContext>
    {
        [System.Serializable]
        internal struct AbilityStage
        {
            [SerializeField] private AbilityAction<TContext> _actions;


            public void Enter(TContext context)
            {
                _actions.Act(context);
            }
        }


        public bool CanCast(TContext context)
        {
            throw new NotImplementedException();
        }

        public bool TryCast(TContext context, out IAbilityLifeHandle handle)
        {
            throw new NotImplementedException();
        }

        
    }
    public abstract class AbilitySo : SoDatabaseElement, IAbility<Character>
    {
        [System.Serializable]
        internal struct AbilityStage
        {
            [SerializeField] private CharacterAbilityAction _actions;


        }


        [SerializeField] private MetaInfo _metaInfo;

        [SerializeField] private DynamicStatAmount[] _cost;
        [SerializeField] private ConstantStatAmount[] _requirements;

        [SerializeField] private AbilityStage[] _stages;
        #region editor
#if UNITY_EDITOR
        [SerializeField] private bool _processStages;
#endif
        #endregion


        internal AbilityStage[] Stages => _stages;


        #region editor
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
        #endregion

        public bool CanCast(Character caster)
        {
            if (!CanCastInternal(caster))
                return false;

            //check stats
            return true;
        }

        public bool TryCast(Character caster, out IAbilityLifeHandle handle)
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


        internal void StartStage(Character caster)
        {
            HandleStageStarted(caster, stage);
        }

        internal void EndStage(Character caster)
        {
            HandleStageEnded(caster, stage);
        }


        protected virtual bool CanCastInternal(Character caster) => true;
    }


}
