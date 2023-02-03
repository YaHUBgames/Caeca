using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Caeca.CustomAddressables;

namespace Caeca.SoundControl.Managers
{
    /// <summary>
    /// Static manager for music play.
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance { private set; get; }

        [Header("References")]
        [SerializeField, Tooltip("Audio source this scripts controls")]
        private AudioSource audioSource;

        private void Awake()
        {
            if (MusicManager.instance != null)
            {
                Destroy(this);
                return;
            }
            MusicManager.instance = this;
        }

        private List<AssetReferenceAudioClip> playedClipsReferences = new List<AssetReferenceAudioClip>();
        private Queue<AssetReferenceAudioClip> playQueue = new Queue<AssetReferenceAudioClip>();

        private AsyncOperationHandle<AudioClip> asyncOperation;
        private AudioClip clip;
        private int assetTimer = 0;


        public void Play(AssetReferenceAudioClip _clipReference)
        {
            if (playedClipsReferences.Contains(_clipReference))
                return;

            playQueue.Enqueue(_clipReference);
            if (playQueue.Count <= 1)
                LoadAndPlaySound();
        }

        private void LoadAndPlaySound()
        {
            AssetReferenceAudioClip clipReference = playQueue.Peek();

            asyncOperation = Addressables.LoadAssetAsync<AudioClip>(clipReference);
            playedClipsReferences.Add(clipReference);

            asyncOperation.Completed += (clipOperation) =>
            {
                clip = clipOperation.Result;
                audioSource.clip = clip;
                assetTimer += Mathf.RoundToInt(clip.length) + 1;

                audioSource.Play();
                StartCoroutine(UnloadAsset());
            };
        }

        private IEnumerator UnloadAsset()
        {
            while (assetTimer > 0)
            {
                yield return new WaitForSeconds(1);
                assetTimer--;
            }
            if (asyncOperation.IsValid())
                Addressables.Release(asyncOperation);

            AssetReferenceAudioClip clipReference = playQueue.Dequeue();
            if (playQueue.Count > 0)
                LoadAndPlaySound();
        }
    }
}
