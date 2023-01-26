using System.Collections;
using UnityEngine;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Controls the volume of an AudioSource and its transition.
    /// </summary>
    public class SoundVolume : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Tooltip("Audio source this scripts controls")]
        private AudioSource audioSource;
        [Header("Settings")]
        [SerializeField, Range(0f, 1f), Tooltip("Max volume this audio can be set to")]
        private float maxVolume = 1f;


        private float volume = 0;
        private Coroutine transition = null;


        public void TurnOff()
        {
            Change(0);
        }

        public void TurnOn()
        {
            Change(maxVolume);
        }

        public void Change(float _volume)
        {
            if (_volume == volume)
                return;
            if (transition is not null)
                StopCoroutine(transition);
            transition = StartCoroutine(Transition(_volume));
        }

        private IEnumerator Transition(float _volume)
        {
            while (_volume != volume)
            {
                yield return new WaitForSeconds(SoundEmittingSettings.volumeTransitionTickLength);
                float delta = _volume - volume;

                volume += delta * Time.deltaTime * SoundEmittingSettings.volumeTransitionSpeed;
                volume = Mathf.Clamp(volume, 0f, maxVolume);

                if (delta <= SoundEmittingSettings.volumeTransitionDeltaMin)
                    volume = _volume;

                audioSource.volume = volume;
            }
        }
    }
}
