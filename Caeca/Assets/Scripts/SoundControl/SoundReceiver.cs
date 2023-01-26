using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.SoundControl
{
    //WiP
    public class SoundReceiver : MonoBehaviour, ISoundReceiving
    {
        private delegate void SoundTick(float _deltaTime);
        private event SoundTick OnSoundTick;

        private delegate void SoundFocus();
        private event SoundFocus BasicSoundWhenFocused;
        private event SoundFocus BasicSoundWhenUnFocused;

        public List<ISoundEmitting> focusableEmitters = new List<ISoundEmitting>();

        [SerializeField] private DebugLogger logger;


        public void TriggerFocusEvent(bool _isfocused)
        {
            if (_isfocused)
            {
                BasicSoundWhenFocused?.Invoke();
                return;
            }
            BasicSoundWhenUnFocused?.Invoke();
        }

        public void TriggerTickEvent(float _deltaTime)
        {
            OnSoundTick?.Invoke(_deltaTime);
        }

        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable)
        {
            OnSoundTick -= _soundEmitter.Tick;
            if (_focusable)
            {
                focusableEmitters.Remove(_soundEmitter);
                return;
            }
            BasicSoundWhenFocused -= _soundEmitter.Unfocus;
            BasicSoundWhenUnFocused -= _soundEmitter.Focus;
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            OnSoundTick += _soundEmitter.Tick;
            if (_focusable)
            {
                focusableEmitters.Add(_soundEmitter);
                return;
            }
            BasicSoundWhenFocused += _soundEmitter.Unfocus;
            BasicSoundWhenUnFocused += _soundEmitter.Focus;
        }

        private void OnTriggerEnter(Collider other)
        {
            ISoundEmitting emitter = other.GetComponent<ISoundEmitting>();
            if (emitter == null)
                return;
            if (emitter.IsActive())
                return;

            EmitterEntered(emitter, emitter.ActivateEmitter(this));
        }

        private void OnTriggerExit(Collider other)
        {
            ISoundEmitting emitter = other.GetComponent<ISoundEmitting>();
            if (emitter == null)
                return;
            if (!emitter.IsActive())
                return;

            EmitterLeft(emitter, emitter.DeactivateEmitter());
        }
    }
}
