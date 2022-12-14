using Game.Core.Abilities;
using Game.Core.TwoDInThreeD;
using UnityEngine;

namespace Game.Core.GameRules
{
    /// <summary>
    /// <see cref="ActiveGameRulesInitializer"/>
    /// </summary>
    public static class ActiveGameRules
    {
        public static ArmorCalculationMethod ArmorCalculationMethod { get; internal set; }
        public static int IgnoreCharactersLayer { get; internal set; }
        public static DirectionsCompositeSo DirectionsComposite { get; internal set; }
        //public static CastStagesSettingsSo CastStagesSettings { get; internal set; }


//#if UNITY_EDITOR
//        public static CastStagesSettingsSo CastStagesSettingsEditor
//            => Object.FindObjectOfType<ActiveGameRulesInitializer>().CastStagesSettings;
//#endif
    }
}
