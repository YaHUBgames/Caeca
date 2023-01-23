namespace Caeca.Interfaces
{
    public interface ISoundEmitting
    {
        public bool ActivateSoundEmitor(ISoundManaging _manager);
        public void TickSoundEmitor(float _deltaTime);
        public void DeactivateSoundEmitor();

        public void FocusOnSound();
        public void FocusedOnDifferentSound();
        public void Unfocus();
    }

    public interface ISoundManaging
    {
        public void SoundEmiterDied(ISoundEmitting _soundEmitter);
    }
}