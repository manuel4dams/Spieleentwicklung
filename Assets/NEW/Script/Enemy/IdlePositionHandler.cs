using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class IdlePositionHandler
    {
        public float waitTimeWhenPositionReached;
        public Vector3 movementBoundsLeft;
        public Vector3 movementBoundsRight;
        public bool startIdlingOnStart;

        public Vector3 nextPosition { get; private set; }
        public event Action idleMovement;

        private float nextIdleMovementTime;

        public void StartIdling()
        {
            nextPosition = Vector3.Lerp(movementBoundsLeft, movementBoundsRight, Random.value);
            nextIdleMovementTime = Time.realtimeSinceStartup + waitTimeWhenPositionReached;
        }

        public void HandleIdleState()
        {
            if (Time.realtimeSinceStartup >= nextIdleMovementTime)
                idleMovement?.Invoke();
        }
    }
}
