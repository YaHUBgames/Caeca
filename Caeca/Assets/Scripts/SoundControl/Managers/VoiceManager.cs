using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Caeca.CustomAddressables;

namespace Caeca.SoundControl.Managers
{
    /// <summary>
    /// Static manager for voiceover.
    /// </summary>
    public class VoiceManager : MonoBehaviour
    {
        public static VoiceManager instance { private set; get; }

        [Header("References")]
        [SerializeField, Tooltip("Audio source this scripts controls")]
        private AudioSource audioSource;

        private void Awake()
        {
            if (VoiceManager.instance != null)
            {
                Destroy(this);
                return;
            }
            VoiceManager.
            instance = this;
        }


        private List<AssetReferenceAudioClip> playedClipsReferences = new List<AssetReferenceAudioClip>();
        private Queue<DelayClipEntry> playQueue = new Queue<DelayClipEntry>();

        private AsyncOperationHandle<AudioClip> asyncOperation;
        private AudioClip clip;
        private int assetTimer = 0;


        public void Play(AssetReferenceAudioClip _clipReference, float _delay, bool _repeatable = false, bool _rewritable = false)
        {
            if (!_repeatable && playedClipsReferences.Contains(_clipReference))
                return;
            if (!_repeatable)
                playedClipsReferences.Add(_clipReference);
            playQueue.Enqueue(new DelayClipEntry(_clipReference, _delay, _rewritable));
            if (playQueue.Count <= 1)
                StartCoroutine(LoadAndPlaySound());
        }

        private IEnumerator LoadAndPlaySound()
        {
            DelayClipEntry thisClip = playQueue.Peek();

            asyncOperation = Addressables.LoadAssetAsync<AudioClip>(thisClip.clipReference);

            yield return new WaitForSeconds(thisClip.delay);

            asyncOperation.Completed += (clipOperation) =>
            {
                clip = clipOperation.Result;
                audioSource.clip = clip;
                assetTimer += Mathf.RoundToInt(clip.length) + 1;

                audioSource.Play();
                StartCoroutine(UnloadAsset(thisClip.rewritable));
            };
        }

        private IEnumerator UnloadAsset(bool _rewritable)
        {
            while (assetTimer > 0)
            {
                yield return new WaitForSeconds(1);
                if (_rewritable && playQueue.Count > 1)
                    assetTimer = 1;
                assetTimer--;
            }
            audioSource.Stop();
            if (asyncOperation.IsValid())
                Addressables.Release(asyncOperation);

            DelayClipEntry thisClip = playQueue.Dequeue();
            if (playQueue.Count > 0)
                StartCoroutine(LoadAndPlaySound());
        }
    }

    public class DelayClipEntry
    {
        public DelayClipEntry(AssetReferenceAudioClip _clipReference, float _delay, bool _rewritable)
        {
            clipReference = _clipReference;
            delay = _delay;
            rewritable = _rewritable;
        }
        public AssetReferenceAudioClip clipReference;
        public float delay;
        public bool rewritable;
    }
}
