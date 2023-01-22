using UnityEngine;

namespace Caeca.TransformMovement
{   
    /// <summary>
    /// Rotates transform around axis.
    /// </summary>
    public class RotateTransform : MonoBehaviour
    {
        [SerializeField, Tooltip("Axis of the rotation")] 
        private Vector3 rotateAxis;
        [SerializeField, Tooltip("Speed the transform will rotate around axis")] 
        private float rotateSpeed = 1f;
        [SerializeField, Tooltip("Transform to move")] 
        private Transform moveTransform;

        private void Update() {
            moveTransform.RotateAround(transform.position, rotateAxis, Time.deltaTime * rotateSpeed);
        }
    }
}
