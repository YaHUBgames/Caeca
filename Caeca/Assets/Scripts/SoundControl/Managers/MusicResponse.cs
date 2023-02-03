using UnityEngine;

using Caeca.CustomAddressables;

namespace Caeca.SoundControl.Managers
{
    /// <summary>
    /// Triggers static music manager.
    /// </summary>
    public class MusicResponse : MonoBehaviour
    {
        [SerializeField] private AssetReferenceAudioClip clipReference;

        public void Respond()
        {
            MusicManager.instance.Play(clipReference);
        }
    }
}
