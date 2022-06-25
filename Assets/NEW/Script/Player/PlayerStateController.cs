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

        public void ApplyHealthChange()
        {
            // TODO Min max logic
            playerState.currentHealth += healthAmount;
            healthChanged = true;
        }
    }
}
