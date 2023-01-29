using UnityEngine;
using System.Collections;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Caeca.ScriptableObjects;
using Caeca.CustomAddressables;

namespace Caeca.SoundControl
{
    /// <summary>
    /// Loads/unloads clipPacks to/from memory and plays random clip from it.
    /// </summary>
    public class SoundPack : MonoBehaviour
    {
        [SerializeField, Tooltip("ClipsPack asstet")]
        private AssetReferenceAudioClipPack clipPackReference;
        private AsyncOperationHandle<AudioClipPack> asyncOperation;
        private AudioClipPack clipPack;
        private int assetTimer = 0;

        [SerializeField, Range(-3, 3)]
        private float randomMinPitch = 1f;
        [SerializeField, Range(-3, 3)]
        private float randomMaxPitch = 1f;

        [SerializeField, Range(0, 60)]
        private float randomMinDelay = 1f;
        [SerializeField, Range(0, 60)]
        private float randomMaxDelay = 1f;

        private float hourglass = 0.1f;

        public async void Tick(AudioSource _source, float _deltaTime)
        {
            if (!asyncOperation.IsValid())
            {
                asyncOperation = Addressables.LoadAssetAsync<AudioClipPack>(clipPackReference);
                asyncOperation.Completed += (clipPackOperation) =>
                {
                    clipPack = clipPackOperation.Result;
                };
                assetTimer = 2;
                StartCoroutine(UnloadAsset());
            }

            if (hourglass > 0)
            {
                hourglass -= _deltaTime;
                return;
            }
            assetTimer = Mathf.RoundToInt(randomMaxDelay + randomMinDelay) + 2;
            await asyncOperation.Task;

            _source.pitch = Random.Range(randomMinPitch, randomMaxPitch);
            _source.PlayOneShot(clipPack.GetRandomClip());

            hourglass += Random.Range(randomMinDelay, randomMaxDelay);
        }
        
        private IEnumerator UnloadAsset()
        {
            while(assetTimer > 0)
            {
                yield return new WaitForSeconds(1);
                assetTimer--;
            }
            if (asyncOperation.IsValid())
                Addressables.Release(asyncOperation);
        }
    }
}
