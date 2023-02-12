using System.Collections.Generic;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that handles focus logic - when and on what to focus.
    /// </summary>
    public class SoundFocuser : MonoBehaviour
    {
        [Header("Controls")]
        [SerializeField] private BoolSO focusControl;
        [SerializeField] private IntSO focusSwitchControl;

        private delegate void SoundFocus();
        private event SoundFocus BasicSoundWhenFocused;
        private event SoundFocus BasicSoundWhenUnFocused;

        private List<ISoundEmitting> focusableEmitters = new List<ISoundEmitting>();
        private bool isFocused = false;
        private int focusedIndex = 0;


        private void Awake()
        {
            focusControl.OnVarSync += OnControlChange;
            focusSwitchControl.OnVarSync += OnSwitchChange;
        }

        private void OnDisable()
        {
            focusControl.OnVarSync -= OnControlChange;
            focusSwitchControl.OnVarSync -= OnSwitchChange;
        }


        public Transform GetCurrentTarget()
        {
            if (HasAnyTarget())
                return focusableEmitters[focusedIndex].GetEmmiter();
            return null;
        }

        public bool HasAnyTarget()
        {
            return focusableEmitters.Count > 0;
        }

        public void OnControlChange(bool _isFocused)
        {
            if (_isFocused == isFocused)
                return;
            if (_isFocused)
            {
                TurnOn();
                return;
            }
            TurnOff();
        }

        private void TurnOn()
        {
            isFocused = true;
            BasicSoundWhenFocused?.Invoke();

            if (!HasAnyTarget())
                return;
            foreach (ISoundEmitting emitter in focusableEmitters)
                emitter.Ignore();

            focusableEmitters[focusedIndex].Focus();
        }

        private void TurnOff()
        {
            isFocused = false;
            BasicSoundWhenUnFocused?.Invoke();

            if (!HasAnyTarget())
                return;
            foreach (ISoundEmitting emitter in focusableEmitters)
                emitter.UnIgnore();
        }

        public void OnSwitchChange(int _newFocusedIndex)
        {
            SwitchFocus(_newFocusedIndex);
        }

        private void SwitchFocus(int _newFocusedIndex)
        {
            if (_newFocusedIndex >= focusableEmitters.Count)
                _newFocusedIndex = 0;
            if (_newFocusedIndex < 0)
                _newFocusedIndex = Mathf.Clamp(focusableEmitters.Count - 1, 0, focusableEmitters.Count);
            if (_newFocusedIndex == focusedIndex)
                return;

            if (isFocused)
            {
                focusableEmitters[focusedIndex].Ignore();
                focusableEmitters[_newFocusedIndex].Focus();
            }

            focusedIndex = _newFocusedIndex;
            focusSwitchControl.ChangeVariable(focusedIndex, true);
        }


        private void EmitterEnteredSetup(ISoundEmitting _soundEmitter)
        {
            if (isFocused)
            {
                _soundEmitter.Ignore();
                return;
            }
            _soundEmitter.UnIgnore();
        }

        private void EmitterLeftCheck(ISoundEmitting _soundEmitter, bool _focusable)
        {
            if (!_focusable)
                return;
            if (focusableEmitters.Count == 1)
                return;

            int leftIndex = focusableEmitters.IndexOf(_soundEmitter);
            if (leftIndex <= focusedIndex)
                focusedIndex--;
            if (focusedIndex < 0)
                focusedIndex = 0;
            focusSwitchControl.ChangeVariable(focusedIndex, true);
        }

        private void EmitterFirstEnteredFocusCheck()
        {
            if (focusableEmitters.Count > 1)
                return;
            focusedIndex = 0;
            focusSwitchControl.ChangeVariable(0, true);
            if (!isFocused)
                return;
            focusableEmitters[focusedIndex].Focus();
        }

        private void EmitterLeftFocusCheck()
        {
            if (!isFocused)
                return;
            focusableEmitters[focusedIndex].Focus();
        }


        public void EmitterLeft(ISoundEmitting _soundEmitter, bool _focusable)
        {
            EmitterLeftCheck(_soundEmitter, _focusable);
            if (_focusable)
            {
                focusableEmitters.Remove(_soundEmitter);
                EmitterLeftFocusCheck();
                return;
            }
            BasicSoundWhenFocused -= _soundEmitter.Ignore;
            BasicSoundWhenUnFocused -= _soundEmitter.UnIgnore;
        }

        public void EmitterEntered(ISoundEmitting _soundEmitter, bool _focusable)
        {
            EmitterEnteredSetup(_soundEmitter);
            if (_focusable)
            {
                focusableEmitters.Add(_soundEmitter);
                EmitterFirstEnteredFocusCheck();
                return;
            }
            BasicSoundWhenFocused += _soundEmitter.Ignore;
            BasicSoundWhenUnFocused += _soundEmitter.UnIgnore;
        }
    }
}
