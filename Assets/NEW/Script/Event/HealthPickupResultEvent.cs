using System;
using GameGraph;
using GameGraph.Common.Blocks;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class HealthPickupResultEvent : SimpleGraphEventBase<HealthPickupResultEvent>
    {
        // Event handling
        public GameObject target;
        public event Action OnEvent;
        public event Action OnFired;

        // Event parameters
        public GameObject source;
        public bool healthPickedUp;

        public void Fire()
        {
            FireEvent(
                target,
                e => e.OnEvent,
                t =>
                {
                    t.source = source;
                    t.healthPickedUp = healthPickedUp;
                });
            OnFired?.Invoke();
        }
    }
}
