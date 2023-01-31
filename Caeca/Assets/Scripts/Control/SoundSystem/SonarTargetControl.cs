using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;
using Caeca.SoundControl;

namespace Caeca.Control.SoundSystem
{
    /// <summary>
    /// Controls the sonar target setting.
    /// </summary>
    public class SonarTargetControl : MonoBehaviour
    {
        [SerializeField] private BoolSO focusControl = default;
        [SerializeField] private VoidSO doInteract;

        [SerializeField] private SoundFocuser soundFocuser;
        [SerializeField] private Transform defaultTarget;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<Transform> -> New target for sonar")]
        private InterfaceObject<GenericInterface<Transform>>[] setNewTarget;


        private void OnValidate()
        {
            foreach (InterfaceObject<GenericInterface<Transform>> interfaceObject in setNewTarget)
                interfaceObject.OnValidate(this);
        }

        private void Awake()
        {
            doInteract.OnEvent += OnInteract;
        }

        private void OnDisable()
        {
            doInteract.OnEvent -= OnInteract;
        }


        public void OnInteract()
        {
            if (focusControl.value)
                GetNewTarget();
        }

        private void GetNewTarget()
        {
            Transform newTarget = soundFocuser.GetCurrentTarget();
            if (newTarget is null)
                newTarget = defaultTarget;
            SetNewTarget(newTarget);
        }

        private void SetNewTarget(Transform _newTarget)
        {
            foreach (InterfaceObject<GenericInterface<Transform>> interfaceObject in setNewTarget)
                interfaceObject.intrfs.TriggerInterface(_newTarget);
        }
    }
}
