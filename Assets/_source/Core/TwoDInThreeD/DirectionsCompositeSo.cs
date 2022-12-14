using UnityEngine;

namespace Game.Core.TwoDInThreeD
{
    [CreateAssetMenu(menuName = "2D in 3D/Directions Composite")]
    public sealed class DirectionsCompositeSo : ScriptableObject
    {
        [SerializeField] private DirectionSo[] _directions;


        public DirectionSo GetClosestDirection(Vector3 desired)
        {
            //variant1: closest angle

            //variant2: closest distance

            return ClosestTo(desired, _directions);
        }


        public static DirectionSo ClosestTo(DirectionSo target, DirectionSo[] variants)
        {
            return ClosestTo(target.VectorDirection, variants);
        }

        public static DirectionSo ClosestTo(Vector3 target, DirectionSo[] variants)
        {
            float closestD = float.PositiveInfinity;
            int closestIndex = -1;

            for (int i = 0; i < variants.Length; i++)
            {
                Vector3 v = variants[i].VectorDirection;

                float d = (v - target).sqrMagnitude;

                if (d < closestD)
                {
                    closestD = d;
                    closestIndex = i;
                }
            }

            return variants[closestIndex]; //out of bounds is ok
        }
    }
}
