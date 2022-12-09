using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Characters
{
    public class CharacterLuckComponent : CharacterComponent
    {
        [SerializeField] private AnimationCurve _luckCurve;

        private System.Random _rnd;


        public event System.Action<float> OnDiceRolled;


        public void SetLuckCurve(AnimationCurve curve) => _luckCurve = curve;


        private void Awake()
        {
            _rnd = new(UnityEngine.Random.Range(0, int.MaxValue));
        }


        public float RollTheDice()
        {
            var luck = RandomHelpers.NextFloat(_rnd, _luckCurve);

            return luck;
        }
    }
}
