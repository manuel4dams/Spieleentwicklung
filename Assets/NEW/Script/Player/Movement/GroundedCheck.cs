using GameGraph;
using JetBrains.Annotations;
using RotaryHeart.Lib.PhysicsExtension;
using UnityEngine;
using Physics = RotaryHeart.Lib.PhysicsExtension.Physics;

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
        public bool hasHitSomething { get; private set; }
        public float distanceToFirstHitGroundObject { get; private set; }

        public void Check()
        {
            hasHitSomething = Physics.SphereCast(
                groundCheckOrigin.position + Vector3.up * playerState.groundCheckRadius,
                playerState.groundCheckRadius,
                Vector3.down,
                out var hitInfo,
                playerState.groundCheckDistance + playerState.groundCheckRadius,
                LayerMask.GetMask(Layer.GROUND),
                PreviewCondition.Editor,
                0f,
                Color.green,
                Color.red);
            
            isGrounded = hasHitSomething && hitInfo.distance <= playerState.groundedDistance;
            distanceToFirstHitGroundObject = hasHitSomething ? hitInfo.distance : playerState.groundCheckDistance;
            
            // Keep for further adjustments
            // Debug.Log($"hit sth {hasHitSomething} - grounded {isGrounded} - distance {distanceToFirstHitGroundObject}");
        }
    }
}
