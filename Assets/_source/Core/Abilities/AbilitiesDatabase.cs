using DevourDev.Unity.ScriptableObjects;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Game/Abilities/Database")]
    public sealed class AbilitiesDatabase : ScriptableObjectsDatabase<AbilitySo>
    {
    }
}
