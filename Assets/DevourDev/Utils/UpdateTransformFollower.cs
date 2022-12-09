using UnityEngine;

namespace DevourDev.Unity.Utils
{
    [RequireComponent(typeof(TransformFollower))]
    public sealed class UpdateTransformFollower : MonoBehaviour
    {
        private TransformFollower _follower;


        private void Start()
        {
            _follower = GetComponent<TransformFollower>();
        }


        private void Update()
        {
            _follower.CheckForSync();
        }
    }
}
