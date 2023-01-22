using UnityEngine;
using UnityEngine.VFX;

using Caeca.Interfaces;

namespace Caeca.vfxControl
{
    /// <summary>
    /// Controls the 8-dir orientation visual effect. Uses (float[], float, float) GenericInterface to control the effect.
    /// </summary>
    public class BaseOrientationVFX : MonoBehaviour, GenericInterface<float[], float, float>
    {
        [Header("Effect to control")]
        [SerializeField, Tooltip("VFX that has 1-4Volume and 5-8Volume Vector4 parameters")]
        private VisualEffect effect;


        private Vector4 first = new Vector4();
        private Vector4 second = new Vector4();

        private int nameID1 = 0;
        private int nameID2 = 0;


        private void Awake()
        {
            nameID1 = Shader.PropertyToID("1-4Volume");
            nameID2 = Shader.PropertyToID("5-8Volume");
        }


        /// <summary>
        /// Gets volume information and controls VFX with it.
        /// </summary>
        /// <param name="value1">Curent values</param>
        /// <param name="value2">Max value</param>
        /// <param name="value3">Offset - unused</param>
        public void TriggerInterface(float[] value1, float value2, float value3)
        {
            first.x = value1[1];
            first.y = value1[0];
            first.z = value1[7];
            first.w = value1[6];

            second.x = value1[5];
            second.y = value1[4];
            second.z = value1[3];
            second.w = value1[2];

            effect.SetVector4(nameID1, Vector4.one - first / value2);
            effect.SetVector4(nameID2, Vector4.one - second / value2);
        }
    }
}