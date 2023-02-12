using UnityEngine;
using UnityEngine.Events;

namespace Caeca.Control
{
    /// <summary>
    /// Needs to get points to be completed, then triggers event
    /// </summary>
    public class TaskDirector : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("How many points are needed to complete this task")]
        private int pointsToSuccess = 1;
        
        [SerializeField] private UnityEvent OnTaskComplete;


        private int currentPoints = 0;


        private void Complete()
        {
            OnTaskComplete?.Invoke();
        }
        

        public void SetTaskPoints(int _points)
        {
            currentPoints = _points;
        }

        public void GiveTaskPoints(int _points)
        {
            currentPoints += _points;
            if (currentPoints >= pointsToSuccess)
                Complete();
        }
    }
}
