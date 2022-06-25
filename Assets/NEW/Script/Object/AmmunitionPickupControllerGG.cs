using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AmmunitionPickupControllerGG
    {
        // References
        public AmmunitionPickup ammunitionPickup;
        public Transform audioSourceTransform;

        // Input
        public PlayerFacade playerFacade;

        // Output
        public event Action restocked;

        public void Restock()
        {
            if (!playerFacade)
                return;

            playerFacade.RestockAmmunition(ammunitionPickup.ammunitionAmount);
        }

        public void RestockHappened()
        {
            AudioSource.PlayClipAtPoint(ammunitionPickup.ammunitionPickupSound, audioSourceTransform.position, 0.4f);
            restocked?.Invoke();
        }
    }
}
