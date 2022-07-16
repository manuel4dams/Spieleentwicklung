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
    }
}
