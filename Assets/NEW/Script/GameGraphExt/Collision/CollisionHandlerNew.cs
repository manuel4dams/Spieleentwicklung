using System;
using GameGraph.Common.Helper;
using JetBrains.Annotations;
using UnityEngine;

namespace GameGraph.Common.Blocks
{
    [GameGraph("Common/Collision")]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class CollisionHandlerNew : IStartHook
    {
        // Output
        public Collision hit { get; private set; }
        public GameObject hitGameObject { get; private set; }
        public event Action onEnter;
        public event Action onStay;
        public event Action onExit;

        // Properties
        public Collider collider { private get; set; }

        [ExcludeFromGraph]
        public void Start()
        {
            if (onEnter != null)
                collider.AddOnCollisionEnterListener(c =>
                {
                    hit = c;
                    hitGameObject = c.gameObject;
                    onEnter?.Invoke();
                });
            if (onStay != null)
                collider.AddOnCollisionStayListener(c =>
                {
                    hit = c;
                    hitGameObject = c.gameObject;
                    onStay?.Invoke();
                });
            if (onExit != null)
                collider.AddOnCollisionExitListener(c =>
                {
                    hit = c;
                    hitGameObject = c.gameObject;
                    onExit?.Invoke();
                });
        }
    }
}
