using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Plays array of loopable sounds with delay from (float) interface. 
    /// </summary>
    public class OrientationSoundPlay : MonoBehaviour, GenericInterface<float>
    {
        [Header("Control")]
        [Tooltip("Controls if the sound showl loop or not")]
        [SerializeField] private BoolSO doPlay = default;

        [Header("Settings")]
        [Tooltip("Length of sound clips in seconds")]
        [SerializeField] private float clipLenth = 2f;

        [Header("References")]
        [Tooltip("Reference to up to 8 sound sources starting with front-left one")]
        [SerializeField] private AudioSource[] audioSources;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<bool> -> Every sound beat triggers with true")]
        private InterfaceObject<GenericInterface<bool>>[] soundBeat;


        private void OnValidate()
        {
            if (soundBeat == null)
                return;
            foreach (InterfaceObject<GenericInterface<bool>> interfaceObject in soundBeat)
                interfaceObject.OnValidate(this);
        }

        private void Start()
        {
            StartCoroutine(PlayOrientationSounds());
        }


        private IEnumerator PlayOrientationSounds()
        {
            while (true)
            {
                yield return new WaitForSeconds(clipLenth);
                yield return new WaitUntil(() => doPlay.value);
                for (int i = 0; i < audioSources.Length; i++)
                    audioSources[i].PlayOneShot(audioSources[i].clip);
                if (soundBeat is not null)
                    foreach (InterfaceObject<GenericInterface<bool>> interfaceObject in soundBeat)
                        interfaceObject.intrfs.TriggerInterface(true);
            }
        }


        /// <summary>
        /// Changes to clip length
        /// </summary>
        /// <param name="value">New clip length</param>
        public void TriggerInterface(float value)
        {
            clipLenth = value;
        }
    }
}
