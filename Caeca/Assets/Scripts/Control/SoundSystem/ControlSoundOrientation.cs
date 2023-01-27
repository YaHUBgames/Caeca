using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Takes player input and controls sound orientation.
    /// </summary>
    public class ControlSoundOrientation : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private BoolSO doPlay8DirOrientation = default;
        [SerializeField] private BoolSO doPlayLedgeOrientation = default;
        [SerializeField] private BoolSO doPlayDirectOrientation = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;

        
        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.Orient.performed += OnDirectOrientPerformed;
            input.Base.Orient.canceled += OnDirectOrientCanceled;

            doPlay8DirOrientation.ChangeVariable(false);
            doPlayLedgeOrientation.ChangeVariable(true);
            doPlayDirectOrientation.ChangeVariable(false);
        }

        private void OnDestroy()
        {
            input.Base.Orient.performed -= OnDirectOrientPerformed;
            input.Base.Orient.canceled -= OnDirectOrientCanceled;
            input.Base.Disable();
        }


        public void OnDirectOrientPerformed(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(true);
            doPlay8DirOrientation.ChangeVariable(true);
            logger.Log("Orientation ON", this);
        }

        public void OnDirectOrientCanceled(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(false);
            doPlay8DirOrientation.ChangeVariable(false);
            logger.Log("Orientation OFF", this);
        }
    }
}
