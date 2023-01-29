using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Sets volume to array of audio sources according to the information received in (float[], float, float) or (float) interface.
    /// </summary>
    public class OrientationSoundVolume : MonoBehaviour, GenericInterface<float[], float, float>, GenericInterface<float>
    {
        [Header("Control")]
        [Tooltip("Controls if the sound should loop or not")]
        [SerializeField] private BoolSO doPlay = default;

        [Header("Sound settings")]
        [Tooltip("Speed in which sounds change their volume")]
        [SerializeField] private float transitionSpeed = 1f;

        [Tooltip("Set logic in which sounds are played")]
        [SerializeField] private bool reverseVolume = false;

        [Tooltip("What volume should be the maximum")]
        [SerializeField, Range(0, 1)] private float maxVolume = 1f;

        [Tooltip("At what volume sound turns off completely")]
        [SerializeField] private float cutOffVolume = 0.01f;

        [Tooltip("At what volume sound start")]
        [SerializeField, Range(0, 1)] private float startVolume = 0f;

        [Tooltip("Reference to up to 8 sound sources starting with front-left one")]
        [SerializeField] private AudioSource[] audioSources;


        private float[] volume;
        private float[] currentVolume;


        private void Awake()
        {
            volume = new float[audioSources.Length];
            currentVolume = new float[audioSources.Length];
            doPlay.OnVarSync += OnControlChange;
        }

        private void OnDisable()
        {
            doPlay.OnVarSync -= OnControlChange;
        }

        private void Start()
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                currentVolume[i] = startVolume;
                volume[i] = startVolume;
                audioSources[i].volume = startVolume;
            }
        }

        private void Update()
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                currentVolume[i] += (volume[i] - currentVolume[i]) * transitionSpeed * Time.deltaTime;
                Mathf.Clamp(currentVolume[i], 0f, maxVolume);
                audioSources[i].volume = currentVolume[i];
            }
        }


        public void OnControlChange(bool _doPlay)
        {
            if (!_doPlay)
                return;
            for (int i = 0; i < audioSources.Length; i++)
                volume[i] = startVolume;
        }


        /// <summary>
        /// Receive volume information.
        /// </summary>
        /// <param name="value1">Unmaped values.</param>
        /// <param name="value2">Max value.</param>
        /// <param name="value3">Shift value.</param>
        public void TriggerInterface(float[] value1, float value2, float value3)
        {
            if(!doPlay.value)
                return;
            for (int i = 0; i < audioSources.Length; i++)
            {
                volume[i] = Mathf.Clamp(((reverseVolume ? value1[i] + value3 : value2 - (value1[i] + value3)) / value2), 0f, maxVolume);
                if (volume[i] < cutOffVolume)
                    volume[i] = 0;
            }
        }

        /// <summary>
        /// Direct alternative to volume information
        /// </summary>
        /// <param name="value">New volume for all audio sources</param>
        public void TriggerInterface(float value)
        {
            if(!doPlay.value)
                return;
            for (int i = 0; i < audioSources.Length; i++)
                volume[i] = Mathf.Clamp(value, 0, 1);
        }
    }
}
