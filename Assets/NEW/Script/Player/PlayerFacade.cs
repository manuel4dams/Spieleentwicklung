using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerFacade : MonoBehaviour
    {
        public event Action healthChange;
        public float healthChangeAmount { get; private set; }

        public event Action restockAmmunition;
        public int restockAmmunitionAmount { get; private set; }

        public void ApplyHealthChange(float amount)
        {
            healthChangeAmount = amount;
            healthChange?.Invoke();
        }

        public void RestockAmmunition(int amount)
        {
            restockAmmunitionAmount = amount;
            restockAmmunition?.Invoke();
        }
    }
}
