using UnityEngine;

namespace DevourDev.Unity.Utils
{
    [RequireComponent(typeof(TransformFollower))]
    public sealed class LateUpdateTransformFollower : MonoBehaviour
    {
        private TransformFollower _follower;


        private void Start()
        {
            _follower = GetComponent<TransformFollower>();
        }


        private void LateUpdate()
        {
            _follower.CheckForSync();
        }
    }
}
