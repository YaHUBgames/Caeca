using UnityEngine;
using UnityEngine.VFX;

using Caeca.Interfaces;

namespace Caeca.vfxControl
{
    /// <summary>
    /// Controls the sonar orientation visual effect. Uses (bool) GenericInterface for triggering sonar effect.
    /// </summary>
    public class SonarOrientationVFX : MonoBehaviour, GenericInterface<bool>
    {
        [Header("Effect to control")]
        [SerializeField, Tooltip("VFX that has SonarDeg int and OnSonarStart event parameters")]
        private VisualEffect effect;

        [Header("Settings")]
        [SerializeField, Tooltip("For how long will the sonar particles be alive")]
        private float sonarVisualLife = 5f;

        [SerializeField, Tooltip("The number of particles on ecah side of current direction")]
        private int sonarVisualSize = 5;

        [SerializeField, Tooltip("Sonar effect reads local euler Y and uses it")]
        private Transform sonarVisualRotationTransform;


        private int nameID1 = 0;
        private int nameID2 = 0;


        private void Awake()
        {
            nameID1 = Shader.PropertyToID("SonarDeg");
            nameID2 = Shader.PropertyToID("OnSonarStart");
            effect.SetInt("SonarSize", sonarVisualSize);
            effect.SetFloat("SonarLife", sonarVisualLife);
        }
        

        /// <summary>
        /// Triggers the soner effect event.
        /// </summary>
        /// <param name="value">Trigger the event if true</param>
        public void TriggerInterface(bool value)
        {
            if (value){
                effect.SetInt(nameID1, (int)Mathf.Repeat(360 - sonarVisualRotationTransform.localEulerAngles.y, 360));
                effect.SendEvent(nameID2);
            }
        }
    }
}
