using UnityEngine;

namespace Caeca.ScriptableObjects
{
    /// <summary>
    /// Scriptable object event synchronization.
    /// </summary>
    public class VariableSO : ScriptableObject
    {
        public delegate void OnEventRaised();
        /// <summary>Event notifying about change.</summary>
        public event OnEventRaised OnVarSync;

        public virtual void ChangeVariable(Object context = null)
        {
            OnVarSync?.Invoke();
        }
    }

    /// <summary>
    /// Scriptable object variable with event synchronization.
    /// </summary>
    /// <typeparam name="T">Type you need.</typeparam>
    [System.Serializable]
    public class VariableSO<T> : ScriptableObject
    {
        public delegate void OnEventRaised(T variable);
        /// <summary>Event notifying about variable change.</summary>
        public event OnEventRaised OnVarSync;
        public T value { get; private set; }

        public virtual void ChangeVariable(T _value, Object context = null)
        {
            if (_value.Equals(value))
                return;
            value = _value;
            OnVarSync?.Invoke(value);
        }
    }

    /// <summary>
    /// Scriptable object variable with event synchronization.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class VariableSO<T1, T2> : ScriptableObject
    {
        public delegate void OnEventRaised(T1 variable1, T2 variable2);
        /// <summary>Event notifying about variables change.</summary>
        public event OnEventRaised OnVarSync;
        public T1 value1 { get; private set; }
        public T2 value2 { get; private set; }

        public virtual void ChangeVariable(T1 _value1, T2 _value2, Object context = null)
        {
            if (_value1.Equals(value1) && _value2.Equals(value2))
                return;
            value1 = _value1;
            value2 = _value2;
            OnVarSync?.Invoke(value1, value2);
        }
    }
}
