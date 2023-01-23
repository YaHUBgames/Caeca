using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.SoundControl
{
    public class SoundEmitorManager : MonoBehaviour, ISoundManaging
    {
        [SerializeField] private LayerMask sphereMask;

        public delegate void SoundTick(float _deltaTime);
        public event SoundTick OnSoundTick;

        public delegate void SoundFocus();
        public event SoundFocus BasicSoundWhenFocused;
        public event SoundFocus BasicSoundWhenUnFocused;

        private List<ISoundEmitting> focusableEmitters = new List<ISoundEmitting>();

        [SerializeField] private int index = -1;

        public void SoundEmiterDied(ISoundEmitting _soundEmitter, bool _focusable)
        {
            OnSoundTick -= _soundEmitter.TickSoundEmitor;
            if (!_focusable)
            {
                BasicSoundWhenFocused -= _soundEmitter.UnfocusSound;
                BasicSoundWhenUnFocused -= _soundEmitter.FocusSound;
            }
            else
            {
                focusableEmitters.Remove(_soundEmitter);
            }
        }

        private void Start()
        {
            StartCoroutine(SlowUpdate());
        }

        private void Cast()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 50f, sphereMask, QueryTriggerInteraction.Collide);
            foreach (Collider collider in colliders)
            {
                ISoundEmitting emittor = collider.GetComponent<ISoundEmitting>();
                if (emittor == null)
                    continue;
                if (emittor.IsActive())
                    continue;

                bool focusable = emittor.ActivateSoundEmitor(this);
                if (focusable)
                {
                    focusableEmitters.Add(emittor);
                }
                else
                {
                    BasicSoundWhenFocused += emittor.UnfocusSound;
                    BasicSoundWhenUnFocused += emittor.FocusSound;
                }

                OnSoundTick += emittor.TickSoundEmitor;
            }
        }

        private IEnumerator SlowUpdate()
        {
            yield return new WaitForSeconds(2f);
            Cast();

            if (index >= 0)
                Focus();
            else
            {
                foreach (ISoundEmitting emitter in focusableEmitters)
                    emitter.FocusSound();
                BasicSoundWhenUnFocused?.Invoke();
            }

            OnSoundTick?.Invoke(2f);

            StartCoroutine(SlowUpdate());
        }

        private void Focus()
        {
            BasicSoundWhenFocused?.Invoke();
            foreach (ISoundEmitting emitter in focusableEmitters)
                emitter.UnfocusSound();
            focusableEmitters[index].FocusSound();
        }
    }
}
