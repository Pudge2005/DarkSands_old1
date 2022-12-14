using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Cast Stage")]
    public sealed class CastStageSo : ScriptableObject
    {
        [SerializeField, HideInInspector] private int _order;
        [SerializeField] private MetaInfo _metaInfo;


        public int Order => _order;

#if UNITY_EDITOR
        internal void SetOrder(int order) => _order = order;
#endif
    }
}
