using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.TransformMovement
{
    /// <summary>
    /// Monobehaviour that smoothly follows transforms position. Use Transform interface to change followTransform.
    /// </summary>
    public class FollowSmoothPosition : MonoBehaviour, GenericInterface<Transform>
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Use Transform interface to change this.")]
        private Transform followTransform;
        [SerializeField]
        private float followSpeed = 1f;

        [SerializeField, Tooltip("Transform to move")]
        private Transform moveTransform;


        private void Update()
        {
            moveTransform.position = Vector3.Lerp(moveTransform.position, followTransform.position, Time.deltaTime * followSpeed);
        }


        /// <summary>
        /// Changes the transform to follow
        /// </summary>
        /// <param name="value">Follow new target</param>
        public void TriggerInterface(Transform value)
        {
            followTransform = value;
        }
    }
}
