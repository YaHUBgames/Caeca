using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.SoundControl;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    public class InteractControl : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private VoidSO doInteract = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;

        [SerializeField] private SoundInteracter soundInteracter;



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
            logger.Log("Interacting");
            doInteract.InvokeSync();
            if (soundInteracter.closestInteractable is not null)
            {
                if(soundInteracter.closestInteractable.Interact())
                {
                    logger.Log("Interacted");
                    return;
                }
                logger.Log("Nothing to interact with nearby");
            }
        }
    }
}
