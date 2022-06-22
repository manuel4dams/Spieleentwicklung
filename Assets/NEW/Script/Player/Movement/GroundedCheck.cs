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
            var hitColliders = new Collider[1];
            Physics.OverlapSphereNonAlloc(groundCheckOrigin.position, playerState.groundColliderRadius, hitColliders,
                LayerMask.GetMask(Layer.GROUND));
            isGrounded = hitColliders[0];
        }
    }
}
