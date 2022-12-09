using UnityEngine;

namespace DevourDev.Unity.Pools
{
    public abstract class ForcedReleasableComponent : MonoBehaviour
    {
        public abstract void Release();
    }
}
