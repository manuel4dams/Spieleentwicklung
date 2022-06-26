using System;
using GameGraph;
using GameGraph.Common.Blocks;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AmmunitionPickupEvent : SimpleGraphEventBase<AmmunitionPickupEvent>
    {
        // Event handling
        public GameObject target;
        public event Action OnEvent;
        public event Action OnFired;

        // Event parameters
        public GameObject source;
        public int ammunitionAmount;

        public void Fire()
        {
            FireEvent(
                target,
                e => e.OnEvent,
                t =>
                {
                    t.source = source;
                    t.ammunitionAmount = ammunitionAmount;
                });
            OnFired?.Invoke();
        }
    }
}
