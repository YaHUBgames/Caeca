using UnityEngine;
using UnityEngine.VFX;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;

namespace Caeca.vfxControl
{
    public class SonarOrientationVFX : MonoBehaviour, GenericInterface<float>, GenericInterface<bool>
    {
        [SerializeField] private VisualEffect effect;

        [Tooltip("Controls if the sound should loop or not")]
        [SerializeField] private BoolSO doPlay = default;

        [SerializeField] private float sonarVisualLife = 5f;
        [SerializeField] private int sonarVisualSize = 5;

        [SerializeField] private Transform sonarVisualRotationTransform;

        public void TriggerInterface(float value)
        {
            effect.SetFloat("SonarSpeed", value);
        }

        private void Awake() {
            doPlay.OnVarSync += SetSonarOnOff;
            SetSonarOnOff(doPlay.value);
        }

        private void OnDisable() {
            doPlay.OnVarSync -= SetSonarOnOff;
        }

        private void Update() {
            effect.SetInt("SonarDeg", (int) Mathf.Repeat(360 - sonarVisualRotationTransform.localEulerAngles.y, 360));
        }

        public void SetSonarOnOff(bool on){
            if(on){
                effect.SetFloat("SonarLife", sonarVisualLife);
                effect.SetInt("SonarSize", sonarVisualSize);
                return;
            }
            effect.SetFloat("SonarLife", 0);
        }

        public void TriggerInterface(bool value)
        {
            if(value)
                effect.SendEvent("OnSonarStart");
        }
    }
}
