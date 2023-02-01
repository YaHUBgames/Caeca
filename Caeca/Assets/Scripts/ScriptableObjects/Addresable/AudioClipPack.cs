using UnityEngine;

namespace Caeca.ScriptableObjects
{
    /// <summary>
    /// Stores array of clips to be as one addressable pack.
    /// </summary>
    [System.Serializable, CreateAssetMenu(menuName = "Scriptable Objects/AudioClipPack")]
    public class AudioClipPack : ScriptableObject
    {
        [Header("Clips")]
        [SerializeField] private int assetLifespan = 1;
        [SerializeField] private AudioClip[] clips;

        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }

        public AudioClip GetRandomClip(int _index)
        {
            return clips[Mathf.Clamp(_index, 0, clips.Length - 1)];
        }

        public int GetAssetLifespan()
        {
            return assetLifespan;
        }
    }
}
