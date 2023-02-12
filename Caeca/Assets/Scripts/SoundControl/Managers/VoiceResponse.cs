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
        [SerializeField] private bool repeatable = false;
        [SerializeField] private bool rewritable = false;

        public void Respond()
        {
            VoiceManager.instance.Play(clipReference, delayTime, repeatable, rewritable);
        }
    }
}
