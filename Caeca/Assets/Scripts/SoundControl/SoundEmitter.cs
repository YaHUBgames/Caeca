using UnityEngine;
using UnityEngine.Events;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that handles events that determine sound behaviour like focus and tick events.
    /// </summary>
    public class SoundEmitter : MonoBehaviour, ISoundEmitting
    {
        [Header("References")]
        [SerializeField] private SoundPlayer soundPlayer;
        [SerializeField] private SoundVolume soundVolume;
        [Header("Settings")]
        [SerializeField, Tooltip("Can player focus on this sound")]
        private bool focusable = false;

        public UnityEvent OnFocused;


        private bool isActive = false;
        private ISoundReceiving manager;


        private void OnDestroy()
        {
            manager?.EmitterLeft((ISoundEmitting)this, focusable);
            isActive = false;
        }


        public bool IsActive()
        {
            return isActive;
        }

        public Transform GetEmmiter()
        {
            return transform;
        }

        public bool ActivateEmitter(ISoundReceiving _manager)
        {
            isActive = true;
            manager = _manager;
            soundVolume.TurnOn();
            return focusable;
        }

        public bool DeactivateEmitter()
        {
            isActive = false;
            manager = null;
            soundVolume.TurnOff();
            return focusable;
        }

        public void Tick(float _deltaTime)
        {
            soundPlayer.Tick(_deltaTime);
        }

        public void Focus()
        {
            UnIgnore();
            OnFocused?.Invoke();
        }

        public void Ignore()
        {
            soundVolume.TurnOff();
        }

        public void UnIgnore()
        {
            soundVolume.TurnOn();
        }
    }
}
