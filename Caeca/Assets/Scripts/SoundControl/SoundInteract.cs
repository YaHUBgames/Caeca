using UnityEngine;
using UnityEngine.Events;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that can be interacted with by the player
    /// </summary>
    public class SoundInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private UnityEvent OnInteract;

        public Transform GetInteractableObject()
        {
            return transform;
        }

        public bool Interact(Transform _interactor)
        {   
            if((_interactor.position - transform.position).magnitude > interactDistance)
                return false;
            Debug.Log(gameObject.name);
            OnInteract?.Invoke();
            return true;
        }
    }
}
