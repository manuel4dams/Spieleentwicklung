using GameGraph;
using JetBrains.Annotations;
using Project;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Object : MonoBehaviour
    {
        [Header("Settings")] //
        public bool destroyable;
        public float healthAmount;
        public GameObject[] containedPickups;
        public AudioClip hitSound;
        public float hitVolume = 2f;

        [Header("References")] //
        public Transform objectTransform;

        public void SpawnContainedPickups()
        {
            foreach (var drop in containedPickups)
            {
                Instantiate(drop, objectTransform.position, objectTransform.rotation);
            }
        }

        public void PlayHitSound()
        {
            MyAudioSource.PlayClipAtPoint(hitSound, objectTransform.position, hitVolume);
        }
    }
}
