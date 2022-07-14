using System;
using GameGraph;
using GameGraph.Common.Helper;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RangeDetector : IStartHook, IUpdateHook
    {
        public Collider followTrigger;
        public Collider stopFollowTrigger;
        public Collider attackTrigger;
        public Collider stopAttackTrigger;
        public bool followDuringAttack;

        public Transform transformToInteract { get; private set; }
        public GameObject objectToInteract { get; private set; }
        public event Action startFollow;
        public event Action doFollow;
        public event Action endFollow;
        public event Action startAttack;
        public event Action doAttack;
        public event Action endAttack;
        public event Action startDoNothing;
        public event Action doNothing;
        public event Action endDoNothing;

        private bool attacking;
        private bool following;
        private bool nothing;

        [ExcludeFromGraph]
        public void Start()
        {
            // TODO Can currently handle only one object at a time, since resetting flags is done for all staying objects
            RegisterAttack();
            RegisterFollow();
        }

        [ExcludeFromGraph]
        public void Update()
        {
            if (!attacking && !following) // -> do nothing
            {
                if (!nothing) // previous do nothing
                    startDoNothing?.Invoke();

                nothing = true;
                doNothing?.Invoke();
            }
            else if (nothing)
            {
                nothing = false;
                endDoNothing?.Invoke();
            }
        }

        private void RegisterFollow()
        {
            followTrigger.AddOnTriggerEnterListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                following = true;
                if (followDuringAttack || !attacking)
                    startFollow?.Invoke();
            });
            stopFollowTrigger.AddOnTriggerStayListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                if (following && (followDuringAttack || !attacking))
                        doFollow?.Invoke();
            });
            stopFollowTrigger.AddOnTriggerExitListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                following = false;
                if (followDuringAttack || !attacking)
                    endFollow?.Invoke();
            });
        }

        private void RegisterAttack()
        {
            attackTrigger.AddOnTriggerEnterListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                attacking = true;
                startAttack?.Invoke();
            });
            stopAttackTrigger.AddOnTriggerStayListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                if (attacking)
                    doAttack?.Invoke();
            });
            stopAttackTrigger.AddOnTriggerExitListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;
                attacking = false;
                endAttack?.Invoke();
            });
        }

        private bool IsObjectOfInterest(Collider c)
        {
            return c.transform.root.CompareTag(Tag.PLAYER);
        }
    }
}
