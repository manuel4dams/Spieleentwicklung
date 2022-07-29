using UnityEngine;

namespace Project
{
    public class MyAudioSource
    {
        public static void PlayClipAtPoint(AudioClip clip, Vector3 position, float loudnessFactor,
            AudioConfiguration.Type type = AudioConfiguration.Type.Sound)
        {
            var volume = AudioConfiguration.GetVolumeForType(type, loudnessFactor);
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}
