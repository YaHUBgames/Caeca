using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Takes player input and controls direct.
    /// </summary>
    public class ControlDirect : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private BoolSO doPlayDirectOrientation = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.Direct.performed += OnDirectPerformed;
            input.Base.Direct.canceled += OnDirectCanceled;

            doPlayDirectOrientation.ChangeVariable(false);
        }

        private void OnDestroy()
        {
            input.Base.Orient.performed -= OnDirectPerformed;
            input.Base.Orient.canceled -= OnDirectCanceled;
            input.Base.Disable();
        }


        public void OnDirectPerformed(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(true);
            logger.Log("Direct ON", this);
        }

        public void OnDirectCanceled(InputAction.CallbackContext context)
        {
            doPlayDirectOrientation.ChangeVariable(false);
            logger.Log("Direct OFF", this);
        }
    }
}
