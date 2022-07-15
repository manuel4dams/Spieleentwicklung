using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class GroundedCheck
    {
        // References
        public PlayerState playerState;
        public Transform groundCheckOrigin;

        // Output
        public bool isGrounded { get; private set; }

        public void Check()
        {
            var o1 = groundCheckOrigin.position;
            var o2 = o1 + Vector3.down * playerState.groundCheckDistance;
            var hitColliders = new Collider[1];
            Physics.OverlapCapsuleNonAlloc(o1, o2, playerState.groundCheckRadius, hitColliders, LayerMask.GetMask(Layer.GROUND));
            isGrounded = hitColliders[0];
        }
    }
}
