using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.SoundControl
{
    /// <summary>
    /// It receives the sound emitters that entered these objects, triggers the collider and send information about these to sound focuser, ticker and interacter.
    /// </summary>
    public class SoundReceiver : MonoBehaviour, ISoundReceiving
    {
        [Header("References")]
        [SerializeField] private SoundFocuser soundFocuser;
        [SerializeField] private SoundInteracter soundInteracter;
        [SerializeField] private SoundTicker soundTicker;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private void OnTriggerEnter(Collider other)
        {
            ISoundEmitting emitter = other.GetComponent<ISoundEmitting>();
            if (emitter is null)
                return;
            if (emitter.IsActive())
                return;

            EmitterEntered(emitter, emitter.ActivateEmitter(this));
        }

        private void OnTriggerExit(Collider other)
        {
            ISoundEmitting emitter = other.GetComponent<ISoundEmitting>();
            if (emitter is null)
                return;
            if (!emitter.IsActive())
                return;

            EmitterLeft(emitter, emitter.DeactivateEmitter());
        }


        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable)
        {
            soundTicker.EmitterLeft(_soundEmitter, _focusable);
            soundFocuser.EmitterLeft(_soundEmitter, _focusable);
            soundInteracter.EmitterLeft(_soundEmitter, _focusable);
            logger.Log("Emitter left");
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            soundTicker.EmitterEntered(_soundEmitter, _focusable);
            soundFocuser.EmitterEntered(_soundEmitter, _focusable);
            soundInteracter.EmitterEntered(_soundEmitter, _focusable);
            logger.Log("Emitter entered");
        }
    }
}
