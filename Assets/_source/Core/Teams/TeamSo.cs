using DevourDev.Unity.ScriptableObjects;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Teams
{
    /// <summary>
    /// Разделение на команды нужно для правильной реакции
    /// врагов на подчиненного ГГем моба и реакции этого моба
    /// на врагов.
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Teams/Team")]
    public sealed class TeamSo : SoDatabaseElement
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private Color _color;


        public MetaInfo MetaInfo => _metaInfo;
        public Color Color => _color;
    }

    public enum DiplomacyAttitude
    {
        Neutral,
        Friends,
        Enemies
    }
}
