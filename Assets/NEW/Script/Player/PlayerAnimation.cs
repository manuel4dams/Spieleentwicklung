using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerAnimation
    {
        // References
        public Animator animator;

        // Input
        public bool sneaking { private get; set; }
        public bool grounded { private get; set; }
        public float distanceToGround { private get; set; }
        public float horizontalMovementSpeed { private get; set; }
        public float verticalSpeed { private get; set; }

        public bool shooting { private get; set; }
        public bool MeleeAttack { private get; set; }

        public void Handle()
        {
            animator.SetFloat("sneaking", sneaking ? 1f : 0f);
            animator.SetBool("grounded", grounded);
            animator.SetFloat("distanceToGround", distanceToGround);
            animator.SetFloat("movementSpeed", horizontalMovementSpeed);
            animator.SetFloat("verticalSpeed", verticalSpeed);
            
            animator.SetFloat("shooting", shooting ? 1f : 0f);
            if (MeleeAttack)
                animator.SetTrigger("Melee");
        }
    }
}
