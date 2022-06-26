using System;
using GameGraph;
using JetBrains.Annotations;

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

        public void ApplyHealthChange()
        {
            healthChanged = ApplyHealthChangeInternal();
            OnHealthChanged?.Invoke();
        }

        private bool ApplyHealthChangeInternal()
        {
            // If we cannot pick up health, skip and notify
            if (playerState.currentHealth >= playerState.maxHealth)
                return false;

            playerState.currentHealth += healthAmount;

            // Cap the health to the max
            if (playerState.currentHealth > playerState.maxHealth)
                playerState.currentHealth = playerState.maxHealth;

            // Return that health was picked up
            return true;
        }
    }
}
