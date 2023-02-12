using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.SoundControl;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Controls the interact action mechanics.
    /// </summary>
    public class InteractControl : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private VoidSO doInteract = default;
        [SerializeField] private SoundInteracter soundInteracter;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.Interact.performed += OnInteractPerformed;
        }

        private void OnDestroy()
        {
            input.Base.Interact.performed -= OnInteractPerformed;
            input.Base.Disable();
        }


        public void OnInteractPerformed(InputAction.CallbackContext context)
        {
            doInteract.InvokeSync();

            if (soundInteracter.Interact())
            {
                logger.Log("Interacted");
                return;
            }
            logger.Log("Nothing to interact with nearby");
        }
    }
}
