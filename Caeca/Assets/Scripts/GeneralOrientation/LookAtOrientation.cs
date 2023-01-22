using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.GeneralOrientation
{
    /// <summary>
    /// Check if orientTransform is looking at target Transform.
    /// </summary>
    public class LookAtOrientation : MonoBehaviour, GenericInterface<Transform>
    {
        [Header("Settings")]
        [SerializeField, Tooltip("How to evaluate the normalized and shifted dot product (0-1)")]
        private AnimationCurve dotToValueCurve;

        [SerializeField, Tooltip("Is orientTransform looking at this")]
        private Transform target;

        [SerializeField, Tooltip("Check if this tranform is looking at target")]
        private Transform orientTransform;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<float> -> dotToValueCurve Evaluation - 1 for looking directly at target, 0 for looking in the opposite direction")]
        private InterfaceObject<GenericInterface<float>>[] lookAtLevel;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        /// <summary>
        /// Changes the target transform
        /// </summary>
        /// <param name="value">New look at target</param>
        public void TriggerInterface(Transform value)
        {
            if (value == null)
            {
                target = orientTransform;
                return;
            }
            target = value;
        }

        private void OnValidate()
        {
            foreach (InterfaceObject<GenericInterface<float>> interfaceObject in lookAtLevel)
                interfaceObject.OnValidate();
        }

        private void Start()
        {
            StartCoroutine(CheckTarget());
        }

        private IEnumerator CheckTarget()
        {
            while (true)
            {
                logger.DebugLine(orientTransform.position, target.position, Color.yellow);
                float value = Vector3.Dot(orientTransform.forward, (target.position - orientTransform.position).normalized) + 1;
                value /= 2;
                value = dotToValueCurve.Evaluate(value);

                foreach (InterfaceObject<GenericInterface<float>> interfaceObject in lookAtLevel)
                    interfaceObject.intrfs.TriggerInterface(value);

                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                yield return new WaitWhile(() => target == orientTransform);
            }
        }
    }
}
