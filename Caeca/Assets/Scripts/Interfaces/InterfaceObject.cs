using UnityEngine;

using Caeca.DebugSystems;

namespace Caeca.Interfaces
{
    /// <summary>
    /// With this you can assign interface in the inspector. 
    /// Use On Validate to be notified when the interface is not correctly assigned.
    /// </summary>
    /// <typeparam name="T">Type of the interface needed.</typeparam>
    [System.Serializable]
    public class InterfaceObject<T>
    {
        [SerializeField] private MonoBehaviour interfaceObject;
        public T intrfs { get; private set; }

        /// <summary>
        /// Validates input and saves the inteface for later use.
        /// </summary>
        /// <param name="context"></param>
        public void OnValidate(Object context = null)
        {
            if (interfaceObject.GetComponent<T>() == null)
            {
                StaticDebugLogger.logger.LogError("Passed object does not contain set interface", context);
                return;
            }
            intrfs = interfaceObject.GetComponent<T>();
        }
    }

    public interface GenericInterface<T>
    {
        public void TriggerInterface(T value);
    }

    public interface GenericInterface<T1, T2>
    {
        public void TriggerInterface(T1 value1, T2 value2);
    }

    public interface GenericInterface<T1, T2, T3>
    {
        public void TriggerInterface(T1 value1, T2 value2, T3 value3);
    }
}
