using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Caeca.CustomAddressables
{
    [System.Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid) { }
    }
}
