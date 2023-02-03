using UnityEngine;
using System.Collections.Generic;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Caeca.ScriptableObjects;
using Caeca.CustomAddressables;

namespace Caeca.TerrainControl
{
    public class GroundTypeSoundPackLoader : MonoBehaviour
    {
        public static GroundTypeSoundPackLoader instance { private set; get; }

        [SerializeField] private AssetReferenceAudioClipPack[] footstepClipPacks;

        
        public List<AudioClipPack> clipPacks = new List<AudioClipPack>();
        [HideInInspector]
        public bool allLoaded = false;

        private List<AsyncOperationHandle<AudioClipPack>> asyncOperations = new List<AsyncOperationHandle<AudioClipPack>>();


        private void Awake()
        {
            if (GroundTypeSoundPackLoader.instance is not null)
            {
                Destroy(this);
                return;
            }
            GroundTypeSoundPackLoader.instance = this;

            LoadAssetsAsync();
        }

        private async void LoadAssetsAsync()
        {
            foreach (AssetReferenceAudioClipPack assetReference in footstepClipPacks)
            {
                AsyncOperationHandle<AudioClipPack> newOp = Addressables.LoadAssetAsync<AudioClipPack>(assetReference);
                asyncOperations.Add(newOp);
                newOp.Completed += (clipPackOperation) =>
                {
                    clipPacks.Add(clipPackOperation.Result);
                };
                await newOp.Task;
            }
            allLoaded = true;
        }

        public static AudioClip GetFootstepClip(GroundTypes _groundType)
        {
            return instance.clipPacks[(int)_groundType].GetRandomClip();
        }

        private void OnDestroy()
        {
            foreach (AsyncOperationHandle<AudioClipPack> asyncOp in instance.asyncOperations)
                if (asyncOp.IsValid())
                    Addressables.Release(asyncOp);
        }
    }
}
