using UnityEngine;

using Caeca.ScriptableObjects;

namespace Caeca.SoundControl.PlayRulesets
{
    /// <summary>
    /// Play ruleset that on asset load starts audio source loop and on asset unload it turns of that loop.
    /// </summary>
    public class RulesetLoopTrigger : PlayRuleset
    {
        private bool isOn = false;

        public override bool CanPlaySound(float _deltaTime)
        {
            return !isOn;
        }

        public override int AssetTimerSetter(int _assetTimer, float _deltaTime)
        {
            return 10;
        }

        public override void PlaySound(AudioSource _source, AudioClipPack _clipPack, float _deltaTime)
        {
            isOn = true;
            _source.clip = _clipPack.GetRandomClip();
            _source.loop = true;
            _source.Play();
        }

        public override void OnAssetUnload(AudioSource _source)
        {
            isOn = false;
            _source.loop = false;
        }
    }
}
