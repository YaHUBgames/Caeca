using UnityEngine;

namespace Caeca.ScriptableObjects
{
    [System.Serializable, CreateAssetMenu(menuName = "Scriptable Objects/AudioClipPack")]
    public class AudioClipPack : ScriptableObject
    {
        [Header("Clips")]
        [SerializeField] private AudioClip[] clips;

        public AudioClip GetRandomClip()
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }
}
