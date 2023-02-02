using UnityEngine;

namespace Caeca.Interfaces
{
    public interface ISoundEmitting
    {
        public bool IsActive();
        public Transform GetEmmiter();

        public bool ActivateEmitter(ISoundReceiving _manager);
        public bool DeactivateEmitter();

        public void Tick(float _deltaTime);

        public void Focus();
        public void Ignore();
        public void UnIgnore();
    }

    public interface ISoundReceiving
    {
        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable);
        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable);
    }
}