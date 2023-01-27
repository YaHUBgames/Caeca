using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca.Control
{
    /// <summary>
    /// Takes player input and controls sound orientation.
    /// </summary>
    public class SoundSystemControls : MonoBehaviour
    {
        [Header("Sound orientation controls")]
        [SerializeField] private BoolSO doPlay8DirOrientation = default;
        [SerializeField] private BoolSO doPlayLedgeOrientation = default;
        [SerializeField] private BoolSO doPlayDirectOrientation = default;
        [SerializeField] private BoolSO doPlaySonar = default;

        [SerializeField] private BoolSO focusControl = default;
        [SerializeField] private IntSO focusSwitch = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;

        
        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.DirectOrient.performed += OnDirectOrientPerformed;
            input.Base.DirectOrient.canceled += OnDirectOrientCanceled;

            input.Base.ShowSonar.performed += OnSonarOrientPerformed;
            input.Base.ShowSonar.canceled += OnSonarOrientCanceled;

            input.Base.Focus.performed += OnFocusPerformed;
            input.Base.Focus.canceled += OnFocusCanceled;
            input.Base.FocusSwitch.performed += OnFocusSwitchPerformed;

            doPlay8DirOrientation.ChangeVariable(true);
            doPlayLedgeOrientation.ChangeVariable(true);
            doPlayDirectOrientation.ChangeVariable(false);
            doPlaySonar.ChangeVariable(false);
        }

        private void OnDestroy()
        {
            input.Base.DirectOrient.performed -= OnDirectOrientPerformed;
            input.Base.DirectOrient.canceled -= OnDirectOrientCanceled;
            input.Base.ShowSonar.performed -= OnSonarOrientPerformed;
            input.Base.ShowSonar.canceled -= OnSonarOrientCanceled;
            input.Base.Focus.performed -= OnFocusPerformed;
            input.Base.Focus.canceled -= OnFocusCanceled;
            input.Base.FocusSwitch.performed -= OnFocusSwitchPerformed;
            input.Base.Disable();
        }

        public void OnDirectOrientPerformed(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(true);
            logger.Log("Direct Orientation ON", this);
        }

        public void OnDirectOrientCanceled(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(false);
            logger.Log("Direct Orientation OFF", this);
        }

        public void OnSonarOrientPerformed(InputAction.CallbackContext context)
        {
            doPlaySonar.ChangeVariable(true);
            logger.Log("Sonar ON", this);
        }

        public void OnSonarOrientCanceled(InputAction.CallbackContext context)
        {
            doPlaySonar.ChangeVariable(false);
            logger.Log("Sonar OFF", this);
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
