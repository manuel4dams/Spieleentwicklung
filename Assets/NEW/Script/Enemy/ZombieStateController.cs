using GameGraph;
using JetBrains.Annotations;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieStateController
    {
        // References
        public ZombieState state;
        public AudioSource audioSource;

        private float nextIdleSound;

        public void HandleZombieDeath()
        {
            //TODO handle zombie death via gamegraph
        }

        public void PlayIdleSound()
        {
            if (state.idleSounds.IsNullOrEmpty())
                return;

            if (!(Time.realtimeSinceStartup >= nextIdleSound)) return;

            // Get Random value from 0 seconds to 4 seconds
            nextIdleSound = Time.realtimeSinceStartup + Random.value * 4f;
            audioSource.clip = state.idleSounds[Random.Range(0, state.idleSounds.Length)];
            audioSource.Play();
        }
    }
}
