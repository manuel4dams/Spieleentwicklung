using System;
using UnityEngine;

namespace Project
{
    public class AudioConfiguration : MonoBehaviour
    {
        [Header("Settings..")] // 
        public float loudnessFactor = 1f;
        public Type type = Type.Sound;
        public bool isPersistent;
        public bool isUniqueGameObjectName;
        public bool ignoreListenerPause;

        [Header("References..")] // 
        public AudioSource audioSource;
        public bool autoAssignSelfAudioSource = true;

        public static event Action notifyVolumeEvent;

        void Start()
        {
            if (autoAssignSelfAudioSource)
                audioSource = GetComponent<AudioSource>();

            // Make this element unique due to the singleton approach via DontDestroyOnLoad
            if (isUniqueGameObjectName)
            {
                var os = GameObject.Find(gameObject.name);
                if (os != gameObject)
                {
                    Destroy(gameObject);
                    return;
                }
            }

            if (isPersistent)
                DontDestroyOnLoad(audioSource.gameObject);

            GetComponent<AudioSource>().ignoreListenerPause = ignoreListenerPause;
            
            ConfigureVolume();            
            notifyVolumeEvent += ConfigureVolume;
        }

        void OnDestroy()
        {
            notifyVolumeEvent -= ConfigureVolume;
        }

        internal void ConfigureVolume()
        {
            audioSource.volume = GetVolumeForType(type, loudnessFactor);
        }

        public static float GetVolumeForType(Type type, float loudnessFactor = 1f)
        {
            // Apply master value
            var volume = loudnessFactor * (Prefs.MasterEnabled.GetBool() ? 1f : 0f) * Prefs.MasterVolume.GetFloat();

            // Apply value for each type
            switch (type)
            {
                case Type.Music:
                    volume *= (Prefs.MusicEnabled.GetBool() ? 1f : 0f) * Prefs.MusicVolume.GetFloat();
                    break;
                case Type.Sound:
                    volume *= (Prefs.SoundsEnabled.GetBool() ? 1f : 0f) * Prefs.SoundsVolume.GetFloat();
                    break;
            }

            return volume;
        }

        public static void NotifyVolume()
        {
            notifyVolumeEvent?.Invoke();
        }

        public enum Type
        {
            Music,
            Sound
        }
    }
}
