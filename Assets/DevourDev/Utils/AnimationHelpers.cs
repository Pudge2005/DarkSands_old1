using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public static class AnimationHelpers
    {
        public static AnimationCurve CurveStartEnd(float startV, float endV)
        {
            return new AnimationCurve(new Keyframe(0, startV), new Keyframe(1f, endV));
        }
    }

}