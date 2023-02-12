using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.Control
{
    /// <summary>
    /// Switches between monobehaviours using index. Only one is enabled at a time.
    /// </summary>
    public class MonobehaviourSwitch : MonoBehaviour, GenericInterface<int>
    {
        [SerializeField, Tooltip("Any monobehaviour that can be disabled")]
        private MonoBehaviour[] monoBehavioursToSwitch;

        [SerializeField, Tooltip("Enabled monobehaviour index")]
        private int currentIndex = 0;


        private void Awake()
        {
            RefreshMonos();
        }


        [ContextMenu("Refresh")]
        void RefreshMonos()
        {
            foreach (MonoBehaviour mono in monoBehavioursToSwitch)
                mono.enabled = false;
            monoBehavioursToSwitch[currentIndex].enabled = true;
        }

        private void SetNewIndex(int newIndex)
        {
            monoBehavioursToSwitch[currentIndex].enabled = false;
            monoBehavioursToSwitch[newIndex].enabled = true;
            currentIndex = newIndex;
        }


        /// <summary>
        /// Change enabled monobehaviour
        /// </summary>
        /// <param name="value">New index</param>
        public void TriggerInterface(int value)
        {
            if (value == currentIndex)
                return;
            SetNewIndex(value);
        }
    }
}
