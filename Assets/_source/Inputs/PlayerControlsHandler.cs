using DevourDev.Unity.InputSystem;
using Game.Core.CharactersControllers;
using UnityEngine;

namespace Game.Inputs
{
    public sealed class PlayerControlsHandler : MonoBehaviour
    {
        [SerializeField] private HorizontalMovementController _horizontalMovementController;
        [SerializeField] private StealthMovementControllerComponent _stealthProcessor;
        [SerializeField] private UseAbilityActionController _dashController;
        [SerializeField] private UseAbilityActionController _normalAttackController;
        [SerializeField] private UseAbilityActionController _heavyAttackController;

        private PlayerControls _playerControls;


        //TODO: decompose handlers  

        private void Awake()
        {
            _playerControls = InputActionsProvider.GetControls<PlayerControls>();

            var defaultControls = _playerControls.Default;
            defaultControls.Attack.performed += HandleAttackPerformed;
            defaultControls.HeavyAttack.performed += HandleHeavyAttackPerformed;
            defaultControls.Dash.performed += HandleDashPerformed;
            defaultControls.Stealth.performed += HandleStealthPerformed;
            defaultControls.Move.performed += HandleMovePerformed;
            defaultControls.Interact.performed += HandleInteractPerformed;
        }

        private void OnDestroy()
        {
            //on application exit raises before on destroy
            if (_playerControls != null)
            {
                var defaultControls = _playerControls.Default;
                defaultControls.Attack.performed -= HandleAttackPerformed;
                defaultControls.HeavyAttack.performed -= HandleHeavyAttackPerformed;
                defaultControls.Dash.performed -= HandleDashPerformed;
                defaultControls.Stealth.performed -= HandleStealthPerformed;
                defaultControls.Move.performed -= HandleMovePerformed;
                defaultControls.Interact.performed -= HandleInteractPerformed;

                InputActionsProvider.ReleaseControls(ref _playerControls);
            }
        }


        private void HandleAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("Attack Performed");
            _normalAttackController.Trigger();
        }

        private void HandleHeavyAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("HeavyAttack Performed");
            _heavyAttackController.Trigger();
        }

        private void HandleDashPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            _dashController.Trigger();
            Debug.Log("Dash Performed");
        }

        private void HandleStealthPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var v = context.ReadValue<float>();
            var stealthInput = v > 0.5f;
            _stealthProcessor.InputValue = stealthInput;

            if (stealthInput)
            {
                Debug.Log("Stealth Performed");
            }
            else
            {
                Debug.Log("Stealth Unperformed");
            }
        }

        private void HandleMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _horizontalMovementController.InputValue = direction;

            if (direction == Vector2.zero)
            {
                Debug.Log("Move: stop");
            }
            else
            {
                Debug.Log("Move: go");
            }
        }

        private void HandleInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("Interaction Performed");
        }

    }
}
