using System.Collections.Generic;
using GameGraph;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptGG
{
    [GameGraph]
    public class PlayerState : MonoBehaviour
    {
        [Header("Health")] //
        public float maxHealth = 10f;
        public bool godMode;
        // [ReadOnly, SerializeField]
        [SerializeField]
        private float currentHealthInternal;
        public float currentHealth { get => currentHealthInternal; protected internal set => currentHealthInternal = value; }
        public bool isAlive => godMode || currentHealthInternal > 0;
        public Image playerDamageIndicatorImage;

        [Header("Visual")] //
        // TODO Rename to prefab
        // TODO Does not belong here
        public GameObject ragDollDead;

        [Header("Movement parameters")] //
        public float runSpeedMultiplier = 2f;
        public float walkSpeedMultiplier = 1f;
        public float turnSpeed = 20;

        [Header("Jump parameters")] //
        public float jumpHeight = 10f;
        public float jumpTimeout = 0.1f;
        public float groundCheckRadius = 0.1f;
        public float groundCheckDistance = 1f;
        public float groundedDistance = 0.2f;

        [Header("Weapons")] //
        [NonReorderable] // Because unity sucks in drawing their UI, disabling reorder fixes a failure where the first entry overlaps some content
        public List<CompoundWeapon> weapons;

        [Header("Weapons")] //
        public AudioClip hitSound;
        public AudioSource audioSource;

        // Controlled state
        // TODO Maybe eliminate this intermediate variable?
        public bool isShooting { get; set; }

        void Start()
        {
            currentHealthInternal = maxHealth;
            audioSource.clip = hitSound;
        }
    }
}
