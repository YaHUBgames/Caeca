using UnityEngine;

namespace Caeca.Interfaces
{
    public interface IInteractable
    {
        public Transform GetInteractableObject();

        public bool Interact() { return false; }
    }
}
