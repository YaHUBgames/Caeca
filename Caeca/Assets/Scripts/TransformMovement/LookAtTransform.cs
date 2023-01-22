using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.TransformMovement
{
    /// <summary>
    /// Monobehaviour that looks at chosen transform. Use Transform interface to change lookAtTransform.
    /// </summary>
    public class LookAtTransform : MonoBehaviour, GenericInterface<Transform>
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Use Transform interface to change this.")]
        private Transform lookAtTransform;

        [SerializeField, Tooltip("Transform to move")]
        private Transform moveTransform;


        private void Update()
        {
            moveTransform.LookAt(lookAtTransform, Vector3.up);
        }


        /// <summary>
        /// Changes the look at transform
        /// </summary>
        /// <param name="value">New look at target</param>
        public void TriggerInterface(Transform value)
        {
            lookAtTransform = value;
        }
    }
}
