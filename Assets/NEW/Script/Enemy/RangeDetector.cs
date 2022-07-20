using System;
using GameGraph;
using GameGraph.Common.Helper;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RangeDetector : IStartHook
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
        private bool previousWasFollowing;

        [ExcludeFromGraph]
        public void Start()
        {
            // TODO Can currently handle only one object at a time, since resetting flags is done for all staying objects
            RegisterAttack();
            RegisterFollow();
            HandleFlags(false, false);
        }

        public void PerformDoEvents()
        {
            if (!objectToInteract.IsUnityNull() && !objectToInteract.activeSelf)
                HandleFlags(false, false);

            if (attacking)
                doAttack?.Invoke();
            if (following)
                doFollow?.Invoke();
            if (nothing)
                doNothing?.Invoke();
        }

        private void RegisterFollow()
        {
            followTrigger.AddOnTriggerEnterListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;

                HandleFlags(true, attacking);
            });
            stopFollowTrigger.AddOnTriggerExitListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;

                HandleFlags(false, false);
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

                previousWasFollowing = following;
                HandleFlags(followDuringAttack && following, true);
            });
            stopAttackTrigger.AddOnTriggerExitListener(c =>
            {
                if (!IsObjectOfInterest(c))
                    return;

                objectToInteract = c.gameObject;
                transformToInteract = c.transform;

                HandleFlags(followDuringAttack ? following : previousWasFollowing, false);
            });
        }

        private bool IsObjectOfInterest(Collider c)
        {
            return c.transform.root.CompareTag(Tag.PLAYER);
        }

        private void HandleFlags(bool newFollowing, bool newAttacking)
        {
            var newNothing = !newFollowing && !newAttacking;

            if (!newAttacking && attacking)
                endAttack?.Invoke();
            if (!newFollowing && following)
                endFollow?.Invoke();
            if (!newNothing && nothing)
                endDoNothing?.Invoke();

            if (newNothing && !nothing)
                startDoNothing?.Invoke();
            if (newFollowing && !following)
                startFollow?.Invoke();
            if (newAttacking && !attacking)
                startAttack?.Invoke();

            nothing = newNothing;
            following = newFollowing;
            attacking = newAttacking;
        }
    }
}
