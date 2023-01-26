using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that is controling the AudioSource. Changes volume, plays sounds and set if sound can be played with (bool) interface.
    /// </summary>
    public class SoundPlayer : MonoBehaviour, GenericInterface<bool>
    {
        [Header("References")]
        [SerializeField, Tooltip("Audio source this scripts controls")]
        private AudioSource audioSource;
        [Header("Settings")]
        [SerializeField, Tooltip("This can be set from outside via <bool> interface")]
        private bool canPlay = true;
        [SerializeField, Tooltip("Sound play settings")]
        private SoundPack soundPack;


        public void Tick(float _deltaTime)
        {
            if(canPlay)
                soundPack.Tick(audioSource, _deltaTime);
        }


        /// <summary>
        /// Controls if the sound can be played
        /// </summary>
        /// <param name="value">Can the sound be played</param>
        public void TriggerInterface(bool value)
        {
            canPlay = value;
        }
    }
}
