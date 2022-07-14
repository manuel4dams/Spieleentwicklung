using System;
using GameGraph;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    public class ZombieMovement
    {
        public Vector3 moveTowardsPosition;
        public Transform moveTowardsTransform;

        public event Action reachedPosition;

        public void MoveTowardsPosition()
        {
            Move(moveTowardsPosition);
        }

        public void MoveTowardsTransform()
        {
            Move(moveTowardsTransform.position);
        }

        private void Move(Vector3 position)
        {
        }
    }
}
