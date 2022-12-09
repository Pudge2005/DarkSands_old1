using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public class MovementProcessorsAutoImporter : MonoBehaviour
    {
        [SerializeField] private MovementHandler _movementHandler;


        private void Start()
        {
            var allComps = gameObject.GetComponents<Component>();
            var handler = _movementHandler;

            foreach (var cmp in allComps)
            {
                if (cmp is IVerticalMovementProcessor vmp)
                    handler.RegisterVerticalMovementProcessor(vmp);

                if (cmp is IHorizontalMovementProcessor hmp)
                    handler.RegisterHorizontalMovementProcessor(hmp);
            }

            Destroy(this);
        }
    }
}
