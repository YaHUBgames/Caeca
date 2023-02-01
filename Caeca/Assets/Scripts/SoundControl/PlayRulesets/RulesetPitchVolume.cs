using UnityEngine;

using Caeca.ScriptableObjects;

namespace Caeca.SoundControl.PlayRulesets
{
    /// <summary>
    /// Play ruleset that plays random clip with random pitch and random delay.
    /// </summary>
    public class RulesetPitchVolume : PlayRuleset
    {
        [SerializeField, Range(-3, 3)]
        private float randomMinPitch = 1f;
        [SerializeField, Range(-3, 3)]
        private float randomMaxPitch = 1f;

        [SerializeField, Range(0, 600)]
        private float randomMinDelay = 1f;
        [SerializeField, Range(0, 600)]
        private float randomMaxDelay = 1f;


        private float hourglass = 0.1f;


        public override bool CanPlaySound(float _deltaTime)
        {
            if (hourglass <= 0)
                return true;
            hourglass -= _deltaTime;
            return false;
        }

        public override int AssetTimerSetter(int _assetTimer, float _deltaTime)
        {
            return _assetTimer;
        }

        public override void PlaySound(AudioSource _source, AudioClipPack _clipPack, float _deltaTime)
        {
            _source.pitch = Random.Range(randomMinPitch, randomMaxPitch);
            _source.PlayOneShot(_clipPack.GetRandomClip());

            hourglass += Random.Range(randomMinDelay, randomMaxDelay);
        }

        public override void OnAssetUnload(AudioSource _source)
        {

        }
    }
}
