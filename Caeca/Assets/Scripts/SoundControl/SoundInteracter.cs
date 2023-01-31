using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl
{
    public class SoundInteracter : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private BoolSO doPlayDirectOrientation = default;

        [Header("Settings")]
        [SerializeField] private Transform interactingObject;
        [SerializeField] private Transform defaultTarget;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<Transform> -> New target for direct")]
        private InterfaceObject<GenericInterface<Transform>>[] setNewTarget;

        private List<IInteractable> interactableEmitters = new List<IInteractable>();
        public IInteractable closestInteractable { get; private set; } = null;
        private bool doDirect = false;


        private void OnValidate() {
            foreach (InterfaceObject<GenericInterface<Transform>> interfaceObject in setNewTarget)
                interfaceObject.OnValidate(this);
        }

        private void Awake()
        {
            doPlayDirectOrientation.OnVarSync += OnDirectControlChange;
        }

        private void OnDisable()
        {
            doPlayDirectOrientation.OnVarSync -= OnDirectControlChange;
        }

        private void Start()
        {
            StartCoroutine(CheckInteractables());
        }


        public void OnDirectControlChange(bool _doDirect)
        {
            doDirect = _doDirect;
        }

        private IEnumerator CheckInteractables()
        {
            yield return new WaitForEndOfFrame();
            while (true)
            {
                yield return new WaitForSeconds(SoundEmittingSettings.emitterTickLength);
                yield return new WaitUntil(() => doDirect);
                SelectClosestInteractable();
            }
        }

        private void SelectClosestInteractable()
        {
            float smallestDistance = float.MaxValue;
            IInteractable newClosestInteractable = null;
            foreach (IInteractable interactable in interactableEmitters)
            {
                float distance = (interactable.GetInteractableObject().position - interactingObject.position).sqrMagnitude;
                if(distance < smallestDistance)
                {
                    smallestDistance = distance;
                    newClosestInteractable = interactable;
                }
            }
            closestInteractable = newClosestInteractable;
            if(closestInteractable is null)
            {
                SetNewTarget(defaultTarget);
                return;
            }
            SetNewTarget(closestInteractable.GetInteractableObject());
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
            if (newSound == closestInteractable)
                closestInteractable = null;
            interactableEmitters.Remove(newSound);
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            IInteractable newSound = _soundEmitter.GetEmmiter().GetComponent<IInteractable>();
            if (newSound is null)
                return;
            interactableEmitters.Add(newSound);
        }
    }
}
