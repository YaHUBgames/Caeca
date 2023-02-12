using UnityEngine;
using UnityEngine.Events;

using Caeca.Interfaces;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that can be interacted with by the player. Triggers events if interaction is/isnt successful.
    /// </summary>
    public class SoundInteract : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private float interactDistance = 2f;
        [SerializeField, Tooltip("How many points are needed to interact with this")]
        private int pointsToSuccess = 1;
        [SerializeField] private UnityEvent OnInteract;
        [SerializeField] private UnityEvent OnCantInteract;


        private int currentPoints = 0;


        public void SetTaskPoints(int _points)
        {
            currentPoints = _points;
        }

        public void GiveTaskPoints(int _points)
        {
            currentPoints += _points;
        }

        public Transform GetInteractableObject()
        {
            return transform;
        }

        public bool Interact(Transform _interactor)
        {
            if ((_interactor.position - transform.position).magnitude > interactDistance)
                return false;
            if (currentPoints >= pointsToSuccess)
            {
                OnInteract?.Invoke();
                return true;
            }
            OnCantInteract?.Invoke();
            return false;
        }
    }
}
