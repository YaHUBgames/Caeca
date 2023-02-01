using UnityEngine;

using Caeca.ScriptableObjects;

namespace Caeca.SoundControl.PlayRulesets
{
    /// <summary>
    /// Base class for sound play behaviour rules.
    /// </summary>
    public class PlayRuleset : MonoBehaviour
    {
        public virtual bool CanPlaySound(float _deltaTime)
        {
            return false;
        }

        public virtual int AssetTimerSetter(int _assetTimer, float _deltaTime)
        {
            return _assetTimer;
        }

        public virtual void PlaySound(AudioSource _source, AudioClipPack _clipPack, float _deltaTime)
        {
            
        }

        public virtual void OnAssetUnload(AudioSource _source)
        {

        }
    }
}
