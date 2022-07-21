using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerStateController
    {
        // References
        public PlayerState state;

        // Health
        public float healthAmount { private get; set; }
        public bool healthChanged { get; private set; }
        public event Action OnHealthChanged;

        // Death
        public GameObject playerDeathParticlesPrefab;
        public Transform playerDeathTransform;
        public Transform playerTransform;
        public event Action OnPlayerDeathParticlesInstantiated;

        public void ApplyHealthChange()
        {
            healthChanged = ApplyHealthChangeInternal();
            OnHealthChanged?.Invoke();
        }

        private bool ApplyHealthChangeInternal()
        {
            // If we cannot pick up health, skip and notify
            if (state.currentHealth >= state.maxHealth && healthAmount >= 0f)
                return false;

            if (healthAmount < 0)
                state.playerDamageIndicatorImage.color = new Color(255f, 255f, 255f, 1f);

            state.currentHealth += healthAmount;

            // Cap the health to the max
            if (state.currentHealth > state.maxHealth)
                state.currentHealth = state.maxHealth;

            // Return that health was picked up
            return true;
        }

        public void InstantiatePlayerDeathParticles()
        {
            UnityEngine.Object.Instantiate(playerDeathParticlesPrefab, playerDeathTransform.position, Quaternion.identity);
            OnPlayerDeathParticlesInstantiated?.Invoke();
        }

        public void UpdateHitIndicator()
        {
            state.playerDamageIndicatorImage.color = Color.Lerp(state.playerDamageIndicatorImage.color, Color.clear, 0.5f);
        }

        public void PlayHitSound()
        {
            state.audioSource.Play();
        }

        public void RagDollDeath()
        {
            // TODO handle in Controller with gamegraph
            var ragDoll = UnityEngine.Object.Instantiate(state.ragDollDead, playerDeathTransform.position, Quaternion.identity);
            var originalJoints = playerTransform.GetComponentsInChildren<Transform>();
            var ragDollJoints = ragDoll.GetComponentsInChildren<Transform>();

            foreach (var originalJoin in originalJoints)
            {
                var found = false;

                foreach (var ragDollJoint in ragDollJoints)
                {
                    if (ragDollJoint.name != originalJoin.name)
                        continue;

                    found = true;
                    ragDollJoint.position = originalJoin.position;
                    ragDollJoint.rotation = originalJoin.rotation;
                    break;
                }

                if (found)
                    break;
            }

            ragDoll.transform.rotation = playerTransform.rotation;
        }
    }
}
