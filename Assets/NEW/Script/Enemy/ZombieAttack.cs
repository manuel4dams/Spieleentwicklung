using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieAttack
    {
        // Input
        public ZombieState state;

        // Output
        public event Action onHit;
        public bool isAttacking { get; private set; }

        private float nextAttackTime;

        public void Attack()
        {
            if (Time.realtimeSinceStartup >= nextAttackTime)
            {
                nextAttackTime = Time.realtimeSinceStartup + state.attackRate;
                PlayAttackSound();
                isAttacking = true;
                onHit?.Invoke();
            }
            else
            {
                isAttacking = false;
            }
        }

        private void PlayAttackSound()
        {
            state.audioSource.clip = state.attackSound;
            state.audioSource.Play();
        }
    }
}
