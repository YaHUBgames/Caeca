using UnityEngine;
using System.Collections;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Caeca.Interfaces;
using Caeca.ScriptableObjects;
using Caeca.CustomAddressables;
using Caeca.SoundControl.PlayRulesets;


namespace Caeca.SoundControl
{
    /// <summary>
    /// Class that is controling the AudioSource with PlayRuleset. Loads asset clipPack. Set if sound can be played with (bool) interface.
    /// </summary>
    [RequireComponent(typeof(PlayRuleset))]
    public class SoundPlayer : MonoBehaviour, GenericInterface<bool>
    {
        [Header("References")]
        [SerializeField, Tooltip("Audio source this scripts controls")]
        private AudioSource audioSource;
        [SerializeField, Tooltip("ClipsPack asstet")]
        private AssetReferenceAudioClipPack clipPackReference;
        [SerializeField, Tooltip("Play sound ruleset")]
        private PlayRuleset playRuleset;

        [Header("Settings")]
        [SerializeField, Tooltip("This can be set from outside via <bool> interface")]

        private bool canPlay = true;
        private AsyncOperationHandle<AudioClipPack> asyncOperation;
        private AudioClipPack clipPack;
        private int assetTimer = 0;


        public async void Tick(float _deltaTime)
        {
            if (!asyncOperation.IsValid())
            {
                asyncOperation = Addressables.LoadAssetAsync<AudioClipPack>(clipPackReference);
                asyncOperation.Completed += (clipPackOperation) =>
                {
                    clipPack = clipPackOperation.Result;
                };
                assetTimer = 10;
                StartCoroutine(UnloadAsset());
            }

            if (!playRuleset.CanPlaySound(_deltaTime) || !canPlay)
            {
                assetTimer = playRuleset.AssetTimerSetter(assetTimer, _deltaTime);
                return;
            }

            await asyncOperation.Task;
            assetTimer = clipPack.GetAssetLifespan();

            playRuleset.PlaySound(audioSource, clipPack, _deltaTime);
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
            playRuleset.OnAssetUnload(audioSource);
        }


        /// <summary>
        /// Controls if the sound can be played
        /// </summary>
        /// <param name="value">Can the sound be played</param>
        public void TriggerInterface(bool value)
        {
            canPlay = value;
        }
    }
}
