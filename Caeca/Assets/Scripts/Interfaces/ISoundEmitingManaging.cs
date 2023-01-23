namespace Caeca.Interfaces
{
    public interface ISoundEmitting
    {
        public bool IsActive();
        
        public bool ActivateSoundEmitor(ISoundManaging _manager);
        public void TickSoundEmitor(float _deltaTime);
        public bool DeactivateSoundEmitor();

        public void FocusSound();
        public void UnfocusSound();
    }

    public interface ISoundManaging
    {
        public void SoundEmiterDied(ISoundEmitting _soundEmitter, bool _focusable);
    }
}