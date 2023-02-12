using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that handles interact logic - when and what can be interacted with.
    /// </summary>
    public class SoundInteracter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform interactingObject;
        [SerializeField] private Transform defaultTarget;
        [SerializeField] private float dotProductMinimumForInteraction = 0.8f;
        [SerializeField] private LayerMask directLineOfSightLayerMask;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<Transform> -> New target for direct")]
        private InterfaceObject<GenericInterface<Transform>>[] setNewTarget;

        private List<IInteractable> interactableEmitters = new List<IInteractable>();
        public IInteractable closestInteractable { get; private set; } = null;


        private void OnValidate()
        {
            foreach (InterfaceObject<GenericInterface<Transform>> interfaceObject in setNewTarget)
                interfaceObject.OnValidate(this);
        }

        private void Start()
        {
            StartCoroutine(CheckInteractablesLoop());
        }


        public bool HasAnyTarget()
        {
            return closestInteractable is not null;
        }

        public bool Interact()
        {
            if (closestInteractable is null)
                return false;
            if (!TargetDotProductCheckSuccessful())
                return false;
            if (!closestInteractable.Interact(transform))
                return false;
            closestInteractable = null;
            return true;
        }


        private IEnumerator CheckInteractablesLoop()
        {
            yield return new WaitForEndOfFrame();
            while (true)
            {
                yield return new WaitForSeconds(SoundEmittingSettings.emitterTickLength);
                SelectClosestInteractable();
            }
        }

        private bool TargetDotProductCheckSuccessful()
        {
            Vector3 interactableDirection = (closestInteractable.GetInteractableObject().position - transform.position).normalized;
            float dotProduct = Vector3.Dot(interactableDirection, transform.forward);
            if (dotProduct < dotProductMinimumForInteraction)
                return false;
            return true;
        }

        private void SelectClosestInteractable()
        {
            float smallestSqrDistance = float.MaxValue;
            IInteractable newClosestInteractable = null;

            foreach (IInteractable interactable in interactableEmitters)
                IsInteractableClosest(interactable, ref newClosestInteractable, ref smallestSqrDistance);

            closestInteractable = newClosestInteractable;
            if (closestInteractable is null)
            {
                SetNewTarget(defaultTarget);
                return;
            }
            SetNewTarget(closestInteractable.GetInteractableObject());
        }

        private bool IsInteractableClosest(IInteractable _interactable, ref IInteractable _newClosestInteractable, ref float _smallestSqrDistance)
        {
            if (_interactable.GetInteractableObject() is null)
                return false;
            float sgrDistance = (_interactable.GetInteractableObject().position - interactingObject.position).sqrMagnitude;
            if (sgrDistance >= _smallestSqrDistance)
                return false;
            if (!RayCheckSuccessful(_interactable))
                return false;

            _smallestSqrDistance = sgrDistance;
            _newClosestInteractable = _interactable;
            return true;
        }

        private bool RayCheckSuccessful(IInteractable _interactable)
        {
            Vector3 interactableDirection = (_interactable.GetInteractableObject().position - transform.position).normalized;
            Ray ray = new Ray(transform.position, interactableDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, 50, directLineOfSightLayerMask, QueryTriggerInteraction.Ignore))
                if (hit.transform == _interactable.GetInteractableObject())
                    return true;
            return false;
        }

        private void SetNewTarget(Transform _newTarget)
        {
            foreach (InterfaceObject<GenericInterface<Transform>> interfaceObject in setNewTarget)
                interfaceObject.intrfs.TriggerInterface(_newTarget);
        }


        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable)
        {
            IInteractable newSound = _soundEmitter.GetEmmiter().GetComponent<IInteractable>();
            if (newSound is null)
                return;
            interactableEmitters.Remove(newSound);
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            IInteractable newSound = _soundEmitter.GetEmmiter().GetComponent<IInteractable>();
            if (newSound is null)
                return;
            interactableEmitters.Add(newSound);
            SelectClosestInteractable();
        }
    }
}
