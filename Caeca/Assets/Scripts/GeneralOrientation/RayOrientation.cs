using System.Collections;
using UnityEngine;

using Caeca.Interfaces;
using Caeca.DebugSystems;

namespace Caeca.GeneralOrientation
{
    /// <summary>
    /// Casts rays in predefined directions(local to this transform), then outputs information about each ray lengths.
    /// </summary>
    public class RayOrientation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Tooltip("Specify any amount of ray directions you want to check")]
        private Vector3[] rayDirections = {
            Vector3.forward + Vector3.left,
            Vector3.forward,
            Vector3.forward + Vector3.right,
            Vector3.right,
            Vector3.back + Vector3.right,
            Vector3.back,
            Vector3.back + Vector3.left,
            Vector3.left
            };

        [SerializeField, Tooltip("Choose what the rays should collide with")]
        private LayerMask raycastMask;

        [SerializeField, Tooltip("Max distance ray will check, if exceeded magnitude will be set to this number")]
        private float maxRayLength = 4f;

        [SerializeField, Tooltip("Value used to offset measured distance")]
        private float rayOffset = 1f;

        [SerializeField, Tooltip("How much should the ray sway down/up from its given direction")]
        private float yChangeForOneUnit = -0.2f;

        [SerializeField, Tooltip("Check if this tranform is looking at target")]
        private Transform orientTransform;

        [Header("OUTPUT")]
        [SerializeField, Tooltip("<float[], float, float> -> Lengths of each array from 0 to maxRayLength, maxRayDistance, rayOffset")]
        private InterfaceObject<GenericInterface<float[], float, float>>[] orientationVolume;

        [Header("Debugging")]
        [SerializeField] private DebugLogger logger;


        private float[] magnitudes;
        private RaycastHit hit = new RaycastHit();


        private void OnValidate()
        {
            foreach (InterfaceObject<GenericInterface<float[], float, float>> interfaceObject in orientationVolume)
                interfaceObject.OnValidate(this);
        }

        private void Awake()
        {
            magnitudes = new float[rayDirections.Length];
        }

        private void Start()
        {
            StartCoroutine(CheckRays());
        }


        private IEnumerator CheckRays()
        {
            while (true)
            {
                for (int i = 0; i < rayDirections.Length; i++)
                    magnitudes[i] = GetRayDistance(i);

                foreach (InterfaceObject<GenericInterface<float[], float, float>> interfaceObject in orientationVolume)
                    interfaceObject.intrfs?.TriggerInterface(magnitudes, maxRayLength, rayOffset);

                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
        }

        private float GetRayDistance(int index)
        {
            Ray ray = new Ray(orientTransform.position, yChangeForOneUnit * Vector3.up +
                GetNormalizedRotatedDirection(rayDirections[index]));

            if (Physics.Raycast(ray, out hit, maxRayLength, raycastMask))
            {
                logger.DebugLine(ray.origin, hit.point, Color.magenta, 0.1f, false);
                return (ray.origin - hit.point).magnitude;
            }
            return maxRayLength;
        }

        private Vector3 GetNormalizedRotatedDirection(Vector3 _direction)
        {
            Vector3 returnValue = _direction;
            returnValue = (returnValue.z * orientTransform.forward + returnValue.x * orientTransform.right).normalized;
            return returnValue.normalized;
        }
    }
}
