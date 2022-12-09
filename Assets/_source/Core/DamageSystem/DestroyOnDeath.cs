using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Core.Characters
{
    [RequireComponent(typeof(HealthComponent))]
    public sealed class DestroyOnDeath : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<HealthComponent>().OnDeath += DestroyOnDeath_OnDeath;
        }

        private void DestroyOnDeath_OnDeath(Character arg1, DynamicStatData arg2, float arg3, float arg4)
        {
            Destroy(gameObject);
        }
    }
}
