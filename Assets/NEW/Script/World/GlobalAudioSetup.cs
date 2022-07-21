using System.Linq;
using UnityEngine;

namespace ScriptGG
{
    [RequireComponent(typeof(AudioSource))]
    public class GlobalAudioSetup : MonoBehaviour
    {
        private void Awake()
        {
            // Make this element unique due to the singleton approach via DontDestroyOnLoad
            var os = FindObjectsOfType<GlobalAudioSetup>();
            if (os.Any(o => o != this))
            {
                Destroy(gameObject);
                return;
            }


            GetComponent<AudioSource>().ignoreListenerPause = true;

            // Make it a singleton
            DontDestroyOnLoad(gameObject);
        }
    }
}
