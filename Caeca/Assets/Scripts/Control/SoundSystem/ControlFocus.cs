using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Takes player input and controls focus.
    /// </summary>
    public class ControlFocus : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private BoolSO focusControl = default;
        [SerializeField] private IntSO focusSwitch = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;

        
        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();

            input.Base.Focus.performed += OnFocusPerformed;
            input.Base.Focus.canceled += OnFocusCanceled;
            input.Base.FocusSwitch.performed += OnFocusSwitchPerformed;

            focusControl.ChangeVariable(false);
            focusSwitch.ChangeVariable(0);
        }

        private void OnDestroy()
        {
            input.Base.Focus.performed -= OnFocusPerformed;
            input.Base.Focus.canceled -= OnFocusCanceled;
            input.Base.FocusSwitch.performed -= OnFocusSwitchPerformed;
            input.Base.Disable();
        }


        public void OnFocusPerformed(InputAction.CallbackContext context)
        {
            focusControl.ChangeVariable(true);
            logger.Log("Focus ON", this);
        }

        public void OnFocusCanceled(InputAction.CallbackContext context)
        {
            focusControl.ChangeVariable(false);
            logger.Log("Focus OFF", this);
        }

        public void OnFocusSwitchPerformed(InputAction.CallbackContext context)
        {
            int dir = Mathf.RoundToInt(context.ReadValue<Vector2>().y);
            focusSwitch.ChangeVariable(focusSwitch.value + dir);
            logger.Log("Focus switched " + dir, this);
        }
    }
}
