using System;
using System.Collections.Generic;
using DevourDev.Base.Collections.Generic;
using DevourDev.Unity.Utils;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    public static class AbilitiesManager
    {
        private class AbilityState : IAbilityLifeHandle
        {
            private AbilitySo _reference;
            private Character _caster;
            private int _currentStageIndex;
            private AbilitySo.AbilityStage[] _stages;
            private int _lastStageIndex;
            private float _cd;

            internal DisorderedArray<AbilityState>.Bucket _bucket;


            public event Action OnCancelled;


            public void Init(AbilitySo reference, Character caster)
            {
                _reference = reference;
                _caster = caster;
                _currentStageIndex = 0;
                _stages = _reference.Stages;
                _lastStageIndex = _stages.Length;
                ResetCD();
            }


            public void Evaluate(float delta)
            {
                if ((_cd -= delta) > 0)
                    return;

                NextStage();
            }



            private void NextStage()
            {
                _reference.EndStage(_caster, _stages[_currentStageIndex].Stage);

                if (_currentStageIndex == _lastStageIndex)
                {
                    Cancel();
                    return;
                }

                _reference.StartStage(_caster, _stages[++_currentStageIndex].Stage);
                ResetCD();
            }

            public void Cancel()
            {
                CleanUp();
                _bucket.RemoveFromArray();

                if (_pool.Count < _defaultCapacity)
                    _pool.Push(_bucket);

                OnCancelled?.Invoke();
            }

            private void ResetCD()
            {
                _cd = _stages[_currentStageIndex].Time;
            }

            private void CleanUp()
            {
                _reference = null;
                _caster = null;
                _stages = null;
            }
        }


        private const int _defaultCapacity = 512;

        private static DisorderedArray<AbilityState> _evaluatingAbilities;
        private static Stack<DisorderedArray<AbilityState>.Bucket> _pool;



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeBeforeScene()
        {
            _evaluatingAbilities = new(_defaultCapacity);
            _pool = new Stack<DisorderedArray<AbilityState>.Bucket>(_defaultCapacity);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InitializeAfterScene()
        {
            Updator.Shared.OnUpdate += HandleUpdate;
        }



        public static IAbilityLifeHandle Cast(AbilitySo ability, Character caster)
        {
            var wrapper = GetStateObject();
            var oneShot = new OneShotCanceller(wrapper.Item);
            wrapper.Item.Init(ability, caster);
            wrapper.ReturnToArray();
            return oneShot;
        }


        private static DisorderedArray<AbilityState>.Bucket GetStateObject()
        {
            if (!_pool.TryPop(out var wrapper))
            {
                wrapper = _evaluatingAbilities.WrapItem(new());
                wrapper.Item._bucket = wrapper;
            }

            return wrapper;
        }

        private static void HandleUpdate()
        {
            float delta = Time.deltaTime;

            foreach (var item in _evaluatingAbilities)
            {
                item.Evaluate(delta);
            }
        }
    }
}
