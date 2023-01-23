using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.SoundControl
{
    public class SoundEmitter : MonoBehaviour, ISoundEmitting
    {
        [SerializeField] private bool focusable = false;
        private bool isActive = false;
        private ISoundManaging manager;

        [SerializeField] private AudioSource source;
        [SerializeField] private Collider soundCollider;

        private void OnDestroy() {
            manager?.SoundEmiterDied((ISoundEmitting)this);
            isActive = false;
            source.enabled = isActive;
            soundCollider.enabled = !isActive;
        }

        public bool ActivateSoundEmitor(ISoundManaging _manager)
        {
            isActive = true;
            source.enabled = isActive;
            soundCollider.enabled = !isActive;
            manager = _manager;
            return focusable;
        }

        public void DeactivateSoundEmitor()
        {
            isActive = false;
            source.enabled = isActive;
            soundCollider.enabled = !isActive;
            manager = null;
        }

        public void TickSoundEmitor(float _deltaTime)
        {
            StartCoroutine(play());
        }

        IEnumerator play(){
            yield return new WaitForSeconds(Random.Range(0f,0.5f));
            source.PlayOneShot(source.clip);
        }

        public void FocusOnSound()
        {
            source.volume = 1;
        }

        public void FocusedOnDifferentSound()
        {
            source.volume = 0;
        }

        public void Unfocus()
        {
            source.volume = 1;
        }
    }
}
