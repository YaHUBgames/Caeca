using UnityEngine;
using UnityEngine.InputSystem;

using Caeca.Input;
using Caeca.DebugSystems;
using Caeca.ScriptableObjects;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Takes player input and controls radar.
    /// </summary>
    public class ControlSonar : MonoBehaviour
    {
        [Header("SControls")]
        [SerializeField] private BoolSO doPlaySonar = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private PlayerInputEvents input;


        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();

            input.Base.Sonar.performed += OnSonarOrientPerformed;
            input.Base.Sonar.canceled += OnSonarOrientCanceled;

            doPlaySonar.ChangeVariable(false);
        }

        private void OnDestroy()
        {
            input.Base.Sonar.performed -= OnSonarOrientPerformed;
            input.Base.Sonar.canceled -= OnSonarOrientCanceled;
            input.Base.Disable();
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
    }
}
