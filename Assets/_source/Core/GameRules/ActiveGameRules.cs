using Game.Core.TwoDInThreeD;

namespace Game.Core.GameRules
{
    public static class ActiveGameRules
    {
        public static ArmorCalculationMethod ArmorCalculationMethod { get; internal set; }
        public static int IgnoreCharactersLayer { get; internal set; }
        public static DirectionsCompositeSo DirectionsComposite { get; internal set; }
    }
}
