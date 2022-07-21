using System;
using GameGraph;
using JetBrains.Annotations;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ZombieMovement
    {
        // References
        public ZombieState zombieState;
        public Rigidbody rigidbody;

        // Input
        public Vector3 moveTowardsPosition;
        public Transform moveTowardsTransform;
        public bool shellRun { private get; set; }

        // Output
        public event Action reachedPosition;
        public bool isWalking { get; private set; }
        public bool isRunning { get; private set; }

        public void MoveTowardsPosition()
        {
            Move(moveTowardsPosition);
        }

        public void MoveTowardsTransform()
        {
            Move(moveTowardsTransform.position);
        }

        public void DoNotMove()
        {
            isWalking = false;
            isRunning = false;
        }

        private void Move(Vector3 position)
        {
            var positionY0 = position.SetY(0f);
            var originY0 = rigidbody.transform.position.SetY(0f);
            var direction = positionY0 - originY0;
            var directionSqrMagnitude = direction.sqrMagnitude;

            if (directionSqrMagnitude <= Constants.FLOATING_PRECISION)
            {
                DoNotMove();
                reachedPosition?.Invoke();
                return;
            }

            // Running
            if (shellRun)
            {
                rigidbody.MovePosition(
                    Vector3.MoveTowards(originY0, positionY0, zombieState.runSpeedMultiplier * Time.deltaTime)
                        .SetY(rigidbody.transform.position.y)
                );
                isWalking = false;
                isRunning = true;
            }
            // Walking
            else
            {
                rigidbody.MovePosition(
                    Vector3.MoveTowards(originY0, positionY0, zombieState.walkSpeedMultiplier * Time.deltaTime)
                        .SetY(rigidbody.transform.position.y)
                );
                isWalking = true;
                isRunning = false;
            }

            Rotate(direction);
        }

        private void Rotate(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            var result = Quaternion.RotateTowards(rigidbody.transform.rotation, targetRotation, zombieState.turnSpeed);
            rigidbody.transform.rotation = result;
        }
    }
}
