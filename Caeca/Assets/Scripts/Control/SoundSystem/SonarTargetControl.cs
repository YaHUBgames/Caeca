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
        [SerializeField] private IntSO focusSwitchControl;
        [SerializeField] private BoolSO doPlaySonar = default;

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
            focusControl.OnVarSync += OnControlChange;
            doPlaySonar.OnVarSync += OnControlChange;
            focusSwitchControl.OnVarSync += OnControlSwitch;
        }

        private void OnDisable()
        {
            focusControl.OnVarSync -= OnControlChange;
            doPlaySonar.OnVarSync -= OnControlChange;
            focusSwitchControl.OnVarSync -= OnControlSwitch;
        }


        public void OnControlChange(bool _unused)
        {
            if (focusControl.value && doPlaySonar.value)
                GetNewTarget();
        }

        public void OnControlSwitch(int _unused)
        {
            OnControlChange(true);
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
