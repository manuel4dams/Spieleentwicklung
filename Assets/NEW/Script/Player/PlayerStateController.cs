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
        public PlayerState playerState;

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
            if (playerState.currentHealth >= playerState.maxHealth && healthAmount >= 0f)
                return false;

            if (healthAmount < 0)
                playerState.playerDamageIndicatorImage.color = new Color(255f, 255f, 255f, 1f);

            playerState.currentHealth += healthAmount;

            // Cap the health to the max
            if (playerState.currentHealth > playerState.maxHealth)
                playerState.currentHealth = playerState.maxHealth;

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
            playerState.playerDamageIndicatorImage.color = Color.Lerp(playerState.playerDamageIndicatorImage.color, Color.clear, 0.5f);
        }
    }
}
