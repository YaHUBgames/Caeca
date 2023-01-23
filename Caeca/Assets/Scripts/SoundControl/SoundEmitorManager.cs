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
        public event SoundFocus OnSoundWhenFocusedOnDifferent;
        public event SoundFocus OnSoundWhenUnFocused;

        private List<ISoundEmitting> emitters = new List<ISoundEmitting>();

        [SerializeField] private int index = -1;

        public void SoundEmiterDied(ISoundEmitting _soundEmitter)
        {
            OnSoundTick -= _soundEmitter.TickSoundEmitor;
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

                bool focusable = emittor.ActivateSoundEmitor(this);
                if (focusable)
                {
                    emitters.Add(emittor);
                }
                else
                {
                    OnSoundWhenFocusedOnDifferent += emittor.FocusedOnDifferentSound;
                    OnSoundWhenUnFocused += emittor.Unfocus;
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
            else{
                foreach (ISoundEmitting emitter in emitters)
                    emitter.Unfocus();
                OnSoundWhenUnFocused?.Invoke();
            }
    
            OnSoundTick?.Invoke(1f);
            
            StartCoroutine(SlowUpdate());
        }

        private void Focus()
        {
            OnSoundWhenFocusedOnDifferent?.Invoke();
            foreach (ISoundEmitting emitter in emitters)
                emitter.FocusedOnDifferentSound();
            emitters[index].FocusOnSound();
        }
    }
}
