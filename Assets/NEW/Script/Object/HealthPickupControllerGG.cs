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
        public PlayerFacade playerFacade;

        // Output
        public event Action onHealed;

        public void Heal()
        {
            if (!playerFacade)
                return;

            playerFacade.ApplyHealthChange(healthPickup.healthAmount);
        }

        public void HealHappened()
        {
            AudioSource.PlayClipAtPoint(healthPickup.healthPickupSound, audioSourceTransform.position, 0.4f);
            onHealed?.Invoke();
        }
    }
}
