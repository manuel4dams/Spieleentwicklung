using System;
using GameGraph;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    public class IdlePositionHandler : IStartHook
    {
        public float waitTimeWhenPositionReached;
        public Transform movementBoundsLeft;
        public Transform movementBoundsRight;
        public bool startIdlingOnStart;

        public Vector3 nextPosition { get; private set; }
        public event Action idleMovement;

        public void CalculateNextPosition()
        {

        }

        public void StartIdling()
        {
            // Set timer after now to start moving
        }

        public void HandleIdleState()
        {

        }

        [ExcludeFromGraph]
        public void Start()
        {
            CalculateNextPosition();
            if(startIdlingOnStart)
                StartIdling();
        }
    }
}
