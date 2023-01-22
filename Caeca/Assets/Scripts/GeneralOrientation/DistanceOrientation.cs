using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.GeneralOrientation
{
    /// <summary>
    /// Check how close is the orientTransform to the target Transform. Use Transform interface to change target Transform.
    /// </summary>
    public class DistanceOrientation : MonoBehaviour, GenericInterface<Transform>
    {
        [Header("Settings")]
        [SerializeField, Tooltip("How to evaluate the raw distance")]
        private AnimationCurve distanceToValueCurve;

        [SerializeField, Tooltip("How close is orientTransform to this Transform")]
        private Transform target;

        [SerializeField, Tooltip("Check far it the target from this Transform")]
        private Transform orientTransform;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<float> -> Evaluation of raw distance with distanceToValueCurve")]
        private InterfaceObject<GenericInterface<float>>[] distanceLevel;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private void OnValidate()
        {
            foreach (InterfaceObject<GenericInterface<float>> interfaceObject in distanceLevel)
                interfaceObject.OnValidate(this);
        }

        private void Start()
        {
            StartCoroutine(CheckTarget());
        }


        private IEnumerator CheckTarget()
        {
            while (true)
            {
                logger.DebugLine(orientTransform.position, target.position, Color.green);
                float value = (target.position - orientTransform.position).magnitude;

                value = distanceToValueCurve.Evaluate(value);

                foreach (InterfaceObject<GenericInterface<float>> interfaceObject in distanceLevel)
                    interfaceObject.intrfs.TriggerInterface(value);

                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
                yield return new WaitWhile(() => target == orientTransform);
            }
        }


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
    }
}
