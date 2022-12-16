using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public class MovementProcessorsComponentsImporterManual : MonoBehaviour
    {
        [SerializeField] private MovementHandler _movementHandler;

        [SerializeField] private MovementProcessorComponent[] _both;
        [SerializeField] private VerticalMovementProcessorComponent[] _verticals;
        [SerializeField] private HorizontalMovementProcessorComponent[] _horizontals;


        private void Start()
        {
            foreach (var b in _both)
            {
                _movementHandler.RegisterVerticalMovementProcessor(b);
                _movementHandler.RegisterHorizontalMovementProcessor(b);
            }

            foreach (var v in _verticals)
            {
                _movementHandler.RegisterVerticalMovementProcessor(v);
            }

            foreach (var h in _horizontals)
            {
                _movementHandler.RegisterHorizontalMovementProcessor(h);
            }

            Destroy(this);
        }
    }
}
