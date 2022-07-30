using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerMovementController
    {
        // References
        public PlayerState playerState;
        public Transform playerTransform;
        public Rigidbody rigidbody;

        // Input
        public float horizontalMovement { private get; set; }
        public bool shellSneak { private get; set; }
        public bool shellJump { private get; set; }
        public bool grounded { private get; set; }

        // Output
        public bool isSneaking { get; private set; }
        public bool isRunning { get; private set; }
        public float currentHorizontalMovementSpeed { get; private set; }
        public float currentVerticalMovement { get; private set; }

        // Variables
        private float nextJumpTimeAllowed;

        public void Move()
        {
            // Walking
            if ((shellSneak || playerState.isShooting) && grounded)
            {
                // TODO Z 0 is bad
                rigidbody.velocity = new Vector3(horizontalMovement * playerState.walkSpeedMultiplier, rigidbody.velocity.y, 0);
                isRunning = false;
                isSneaking = true;
            }
            // Running
            else
            {
                // TODO Z 0 is bad
                rigidbody.velocity = new Vector3(horizontalMovement * playerState.runSpeedMultiplier, rigidbody.velocity.y, 0);
                isRunning = Mathf.Abs(horizontalMovement) != 0f;
                isSneaking = false;
            }

            // Handle jump
            if (grounded && shellJump && Time.realtimeSinceStartup > nextJumpTimeAllowed)
            {
                nextJumpTimeAllowed = Time.realtimeSinceStartup + playerState.jumpTimeout;
                rigidbody.AddForce(new Vector3(0, playerState.jumpHeight, 0));
            }

            // TODO Was only horizontal movement without runSpeedMultiplier before
            //      Test this
            currentHorizontalMovementSpeed = Mathf.Abs(rigidbody.velocity.x);
            currentVerticalMovement = rigidbody.velocity.y;

            Rotate();
        }

        private void Rotate()
        {
            if (horizontalMovement == 0f)
            {
                // Rotate towards the closest direction from the point where the player is currently facing to fully turn left or right if we stop
                // movement during turn phase
                var directionLeftOrRight = Mathf.Sign(playerTransform.forward.z);
                var quaternionLeftOrRight = Quaternion.LookRotation(Vector3.forward * directionLeftOrRight);
                var resultLeftOrRight = Quaternion.RotateTowards(playerTransform.rotation, quaternionLeftOrRight, playerState.turnSpeed);
                playerTransform.rotation = resultLeftOrRight;

                // Do not handle normally rotation if we are standing still
                return;
            }

            var targetRotation = Quaternion.LookRotation(Vector3.forward * Mathf.Sign(horizontalMovement));
            var result = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, playerState.turnSpeed);
            playerTransform.rotation = result;
        }
    }
}
