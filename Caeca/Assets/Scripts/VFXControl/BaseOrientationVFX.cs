using UnityEngine;
using UnityEngine.VFX;

using Caeca.Interfaces;

namespace Caeca.vfxControl
{
    public class BaseOrientationVFX : MonoBehaviour, GenericInterface<float[], float, float>
    {
        [SerializeField] private VisualEffect effect;

        Vector4 first = new Vector4();
        Vector4 second = new Vector4();

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

            effect.SetVector4("1-4Volume", Vector4.one - first / value2);
            effect.SetVector4("5-8Volume", Vector4.one - second / value2);
        }
    }
}