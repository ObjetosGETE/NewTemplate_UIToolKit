using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(menuName = "AudioEvents/Simple",fileName = "NewSimpleAudioEvent")]
    public class SimpleAudioEvent : AudioEvent
    {
        public AudioClip clip;

        [Range(0f,3f)]
        public float volume;
    
        [Range(0f,2f)]
        public float pitch;

        public override void Play(AudioSource source)
        {
            source.volume = volume;
            source.pitch = pitch;
            source.clip = clip;
            source.outputAudioMixerGroup = mixerGroup;
            source.Play();
        }
    }
}
