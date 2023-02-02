using UnityEngine;

using Caeca.CustomAddressables;

namespace Caeca.SoundControl.Managers
{
    /// <summary>
    /// Triggers static voice manager.
    /// </summary>
    public class VoiceResponse : MonoBehaviour
    {
        [SerializeField] private AssetReferenceAudioClip clipReference;
        [SerializeField] private float delayTime;

        public void Respond()
        {
            VoiceManager.Instance.Play(clipReference, delayTime);
        }
    }
}
