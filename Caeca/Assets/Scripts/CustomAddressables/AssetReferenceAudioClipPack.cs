using UnityEngine.AddressableAssets;

using Caeca.ScriptableObjects;

namespace Caeca.CustomAddressables
{
    [System.Serializable]
    public class AssetReferenceAudioClipPack : AssetReferenceT<AudioClipPack>
    {
        public AssetReferenceAudioClipPack(string guid) : base(guid) { }
    }
}
