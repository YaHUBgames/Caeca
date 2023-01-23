using System.Collections;
using UnityEngine;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that is controling the audioSource. Changes volume and plays sounds.
    /// </summary>
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
/*class soundPack - random clip,pich max volume etc*/

        private float volume = 0;
        private Coroutine volumeTransition = null;

/**/
        public void TickSound(float _deltaTime)
        {
            StartCoroutine(PlaySound());
        }

        private IEnumerator PlaySound()
        {
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            audioSource.PlayOneShot(audioSource.clip);
        }/**/

        public void ChangeVolume(float _volume)
        {
            if (volumeTransition != null)
                StopCoroutine(volumeTransition);
            volumeTransition = StartCoroutine(VolumeTransition(_volume));
        }

        private IEnumerator VolumeTransition(float _volume)
        {
            while (_volume != volume)
            {
                yield return new WaitForSeconds(SoundEmittingSettings.volumeTransitionTickLength);
                float delta = _volume - volume;
                volume += delta * Time.deltaTime * SoundEmittingSettings.volumeTransitionSpeed;
                volume = Mathf.Clamp(volume, 0f, 1f);
                if (delta <= 0.05f)
                    volume = _volume;
                audioSource.volume = volume;
            }
        }
    }
}
