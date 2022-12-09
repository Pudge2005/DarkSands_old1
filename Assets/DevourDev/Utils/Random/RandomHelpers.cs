using UnityEngine;

namespace DevourDev.Unity.Utils
{
    public static class RandomHelpers
    {
        private static readonly System.Random _rndInst = new();


        public static Vector2 PointOnCircle(System.Random rndInst)
        {
            double angle = rndInst.NextDouble() * System.Math.PI * 2;
            Vector2 p;
            p.x = (float)System.Math.Cos(angle);
            p.y = (float)System.Math.Sin(angle);
            return p;
        }

        public static float NextFloat(float min, float max, System.Random rnd, UnityEngine.AnimationCurve curve)
        {
            float mantissa = NextFloat(rnd, curve);
            //range: from 15 to 20
            //mantissa: 0.5f;
            //expected result: (20 - 15) * 0.5 + 15
            //                  5 * 0.5 + 15
            //                  2.5 + 15 = 17.5
            return (max - min) * mantissa + min;
        }


        public static float NextFloat(System.Random rnd, UnityEngine.AnimationCurve curve)
        {
            return curve.Evaluate(NextFloat(rnd));
        }

        public static float NextFloat(System.Random rnd)
        {
            return (float)rnd.NextDouble();
        }

        public static float NextFloat()
        {
            return NextFloat(_rndInst);
        }

        public static float NextFloat(UnityEngine.AnimationCurve curve)
        {
            return NextFloat(_rndInst, curve);
        }
    }
}
