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
        private PlayerInputEvents input;

        [Header("Sound orientation controls")]
        [SerializeField] private BoolSO doPlay8DirOrientation = default;
        [SerializeField] private BoolSO doPlayLedgeOrientation = default;
        [SerializeField] private BoolSO doPlayDirectOrientation = default;
        [SerializeField] private BoolSO doPlaySonar = default;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;

        private void Awake()
        {
            input = new PlayerInputEvents();
            input.Base.Enable();
            input.Base.DirectOrient.performed += OnDirectOrientPerformed;
            input.Base.DirectOrient.canceled += OnDirectOrientCanceled;

            input.Base.ShowSonar.performed += OnSonarOrientPerformed;
            input.Base.ShowSonar.canceled += OnSonarOrientCanceled;

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
    }
}
