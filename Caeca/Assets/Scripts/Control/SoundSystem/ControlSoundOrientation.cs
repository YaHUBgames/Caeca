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

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.Orient.performed += OnOrientPerformed;
            input.Base.Orient.canceled += OnOrientCanceled;

            doPlay8DirOrientation.ChangeVariable(false);
            doPlayLedgeOrientation.ChangeVariable(true);
        }

        private void OnDestroy()
        {
            input.Base.Orient.performed -= OnOrientPerformed;
            input.Base.Orient.canceled -= OnOrientCanceled;
            input.Base.Disable();
        }


        public void OnOrientPerformed(InputAction.CallbackContext context)
        {
            doPlay8DirOrientation.ChangeVariable(true);
            logger.Log("Orientation ON", this);
        }

        public void OnOrientCanceled(InputAction.CallbackContext context)
        {
            doPlay8DirOrientation.ChangeVariable(false);
            logger.Log("Orientation OFF", this);
        }
    }
}
