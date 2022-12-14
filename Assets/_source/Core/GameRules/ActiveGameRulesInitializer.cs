using System;
using Game.Core.Abilities;
using Game.Core.TwoDInThreeD;
using UnityEngine;

namespace Game.Core.GameRules
{
    public sealed class ActiveGameRulesInitializer : MonoBehaviour
    {
        [SerializeField] private ArmorCalculationMethod _armorCalculationMethod;
        [SerializeField] private LayerMask _ignoreCharactersLayer;
        [SerializeField] private DirectionsCompositeSo _directionsComposite;
        //[SerializeField] private CastStagesSettingsSo _castStagesSettings;


#if UNITY_EDITOR
        public ArmorCalculationMethod ArmorCalculationMethod => _armorCalculationMethod;
        public LayerMask IgnoreCharactersLayer => _ignoreCharactersLayer;
        public DirectionsCompositeSo DirectionsComposite => _directionsComposite;
        //public CastStagesSettingsSo CastStagesSettings => _castStagesSettings;
#endif


        private void Awake()
        {
            ActiveGameRules.ArmorCalculationMethod = _armorCalculationMethod;
            int layerID = -1;
            int bitMask = _ignoreCharactersLayer;

            for (int i = 0; i < 32; i++)
            {
                if (CheckBit(bitMask, i))
                {
                    layerID = i;
                    break;
                }
            }

            ActiveGameRules.IgnoreCharactersLayer = layerID;
            ActiveGameRules.DirectionsComposite = _directionsComposite;
            //ActiveGameRules.CastStagesSettings = _castStagesSettings;
        }

        private static bool CheckBit(int value, int bitPos)
        {
            int x = value >> bitPos;
            return (x & 1) == 1;
        }
    }
}
