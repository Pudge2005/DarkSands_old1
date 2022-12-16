using UnityEngine;

namespace Game.Core.TwoDInThreeD
{
    [CreateAssetMenu(menuName = "2D in 3D/Direction")]
    public sealed class DirectionSo : ScriptableObject
    {
        [SerializeField] private Vector3 _vectorDirection;
#if UNITY_EDITOR
        [SerializeField] private bool _normalize;
#endif


        public Vector3 VectorDirection => _vectorDirection;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_normalize)
            {
                _normalize = false;
                _vectorDirection = _vectorDirection.normalized;
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
