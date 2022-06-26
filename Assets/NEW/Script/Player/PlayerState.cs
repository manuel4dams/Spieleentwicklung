using System.Collections.Generic;
using GameGraph;
using MyBox;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    public class PlayerState : MonoBehaviour
    {
        [Header("Health")] //
        public float maxHealth = 10f;
        public bool godMode = false;
        [ReadOnly, SerializeField]
        private float currentHealthInternal;
        public float currentHealth { get => currentHealthInternal; protected internal set => currentHealthInternal = value; }
        public bool isAlive => currentHealthInternal > 0;

        [Header("Movement parameters")] //
        public float runSpeedMultiplier = 2f;
        public float walkSpeedMultiplier = 1f;
        public float turnSpeed = 20;

        [Header("Jump parameters")] //
        public float jumpHeight = 10f;
        public float jumpTimeout = 0.1f;
        public float groundColliderRadius = 0.2f;

        [Header("Weapons")] //
        [NonReorderable] // Because unity sucks in drawing their UI, disabling reorder fixes a failure where the first entry overlaps some content
        public List<CompoundWeapon> weapons;

        // Controlled state
        // TODO Maybe eliminate this intermediate variable?
        public bool isShooting { get; set; }

        void Start()
        {
            currentHealthInternal = maxHealth;
        }
    }
}
