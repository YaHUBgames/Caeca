using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that handles events that determine sound behaviour like focus and tick events.
    /// </summary>
    public class SoundEmitter : MonoBehaviour, ISoundEmitting
    {
        [SerializeField] private bool focusable = false;
        [SerializeField] private SoundPlayer soundPlayer;


        private bool isActive = false;
        private ISoundManaging manager;


        private void OnDestroy()
        {
            manager?.SoundEmiterDied((ISoundEmitting)this, focusable);
            isActive = false;
        }


        private void SetVolume(float _volume)
        {
            soundPlayer.ChangeVolume(_volume);
        }


        public bool IsActive()
        {
            return isActive;
        }

        public bool ActivateSoundEmitor(ISoundManaging _manager)
        {
            isActive = true;
            manager = _manager;
            SetVolume(1);
            return focusable;
        }

        public bool DeactivateSoundEmitor()
        {
            isActive = false;
            manager = null;
            SetVolume(0);
            return focusable;
        }

        public void TickSoundEmitor(float _deltaTime)
        {
            soundPlayer.TickSound(_deltaTime);
        }

        public void FocusSound()
        {
            SetVolume(1);
        }

        public void UnfocusSound()
        {
            SetVolume(0);
        }
    }
}
