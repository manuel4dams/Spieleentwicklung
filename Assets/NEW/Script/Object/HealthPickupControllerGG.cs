using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HealthPickupControllerGG
    {
        // References 
        public HealthPickup healthPickup;
        public Transform audioSourceTransform;

        // Input
        public PlayerState playerState;

        // Output
        public event Action onHealed;

        public void Heal()
        {
            if (!playerState)
                return;

            var healthApplied = playerState.ApplyHealthChange(healthPickup.healthAmount);
            if (healthApplied)
            {
                AudioSource.PlayClipAtPoint(healthPickup.healthPickupSound, audioSourceTransform.position, 0.4f);
                onHealed?.Invoke();
            }
        }
    }
}
