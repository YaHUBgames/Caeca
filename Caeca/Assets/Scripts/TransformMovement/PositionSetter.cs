using UnityEngine;

namespace Caeca.TransformMovement
{
    /// <summary>
    /// Preset positions and then move transform to those positions.
    /// </summary>
    public class PositionSetter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Preset any positions move transform will move to")]
        private Vector3[] presetPositions;

        [SerializeField, Tooltip("Transform to move")]
        private Transform moveTransform;

        public void SetPosition(int _presetPositionIndex)
        {
            if(presetPositions.Length <= 0)
                return;
            _presetPositionIndex = Mathf.Clamp(_presetPositionIndex, 0, presetPositions.Length);
            moveTransform.position = presetPositions[_presetPositionIndex];
        }
    }
}
