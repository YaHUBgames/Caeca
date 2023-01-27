using UnityEngine;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Stores audio clips and decides what to play.
    /// </summary>
    /// 
    /// WiP
    [System.Serializable]
    public class SoundPack
    {
        [SerializeField, Tooltip("Clips that will be played randomly")]
        private AudioClip[] clips; // ADRESABLES !!

        [SerializeField, Range(-3,3)] 
        private float randomMinPitch = 1f;
        [SerializeField, Range(-3,3)] 
        private float randomMaxPitch = 1f;

        [SerializeField, Range(0,60)] 
        private float randomMinDelay = 1f;
        [SerializeField, Range(0,60)] 
        private float randomMaxDelay = 1f;

        private float hourglass = 0f;

        public void Tick(AudioSource _source, float _deltaTime){
            if(hourglass > 0){
                hourglass -= _deltaTime;
                return;
            }
            
            _source.pitch = Random.Range(randomMinPitch, randomMaxPitch);
            _source.PlayOneShot(GetRandomClip());

            hourglass += Random.Range(randomMinDelay, randomMaxDelay);
        }

        private AudioClip GetRandomClip(){
            return clips[Random.Range(0, clips.Length)];
        }
    }
}
