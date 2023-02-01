using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that can be interacted with by the player
    /// </summary>
    public class SoundInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private float interactDistance = 2f;

        public Transform GetInteractableObject()
        {
            return transform;
        }

        public bool Interact(Transform _interactor)
        {   
            if((_interactor.position - transform.position).magnitude > interactDistance)
                return false;
            Debug.Log(gameObject.name);
            return true;
        }
    }
}
