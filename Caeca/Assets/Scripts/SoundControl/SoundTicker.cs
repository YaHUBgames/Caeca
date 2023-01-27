using System.Collections;
using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// The class that is responsible for propagating the elapsed time to any listeners with a SlowUpdate
    /// </summary>
    public class SoundTicker : MonoBehaviour
    {
        private delegate void SoundTick(float _deltaTime);
        private event SoundTick OnSoundTick;


        private void Start()
        {
            StartCoroutine(SlowUpdate());
        }


        private IEnumerator SlowUpdate()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                OnSoundTick?.Invoke(0.1f);
            }
        }


        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable)
        {
            OnSoundTick -= _soundEmitter.Tick;
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            OnSoundTick += _soundEmitter.Tick;
        }
    }
}
