using UnityEngine;

using Caeca.Interfaces;

namespace Caeca.TransformMovement
{
    /// <summary>
    /// Monobehaviour that follows chosen aspects of a transform. Use Transform interface to change followTransform.
    /// </summary>
    public class FollowTransform : MonoBehaviour, GenericInterface<Transform>
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Use Transform interface to change this.")]
        private Transform followTransform;

        [SerializeField, Tooltip("Transform to move")]
        private Transform moveTransform;


        [HideInInspector] public bool[] followPosition = new bool[3];
        [HideInInspector] public bool[] followRotation = new bool[3];


        private void Update()
        {
            moveTransform.position = new Vector3(followPosition[0] ? followTransform.position.x : moveTransform.position.x,
                followPosition[1] ? followTransform.position.y : moveTransform.position.y,
                followPosition[2] ? followTransform.position.z : moveTransform.position.z);
            moveTransform.rotation = Quaternion.Euler(followRotation[0] ? followTransform.eulerAngles.x : moveTransform.eulerAngles.x,
                followRotation[1] ? followTransform.eulerAngles.y : moveTransform.eulerAngles.y,
                followRotation[2] ? followTransform.eulerAngles.z : moveTransform.eulerAngles.z);
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
