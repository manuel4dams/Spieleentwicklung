using System.Collections.Generic;
using MyBox;
using UnityEngine;
using AudioConfiguration = Project.AudioConfiguration;

namespace ScriptGG
{
    public abstract class WeaponGG : MonoBehaviour
    {
        [Header("General weapon settings")] //
        public float fireRate = 0.15f;
        public AudioClip shootSound;
        public AudioClip reloadSound;
        public AudioClip equipSound;
        public bool ammunitionEnabled = true;
        [ConditionalField(nameof(ammunitionEnabled))] //
        public int startRounds = 100;
        [ConditionalField(nameof(ammunitionEnabled))] //
        public int maxRounds = 100;
        public int reloadAmountMultiplier = 1;

        [Header("References")] //
        public Transform weaponOrigin;
        public List<GameObject> weaponVisualContainers;
        [Tooltip("The source where the sound is coming from when using this weapon")] //
        public AudioSource weaponAudioSource;

        // Variables
        private float timeNextBulletAllowed;
        [SerializeField, ReadOnly, ConditionalField(nameof(ammunitionEnabled))] //
        private int remainingRoundsInternal;
        public int remainingRounds => remainingRoundsInternal;

        void Start()
        {
            remainingRoundsInternal = startRounds;
        }

        public void Equip()
        {
            // Play equip sound
            if (equipSound)
            {
                // Hot fix for weapon sound is way to loud
                weaponAudioSource.GetComponent<AudioConfiguration>().ConfigureVolume();
                
                weaponAudioSource.clip = equipSound;
                weaponAudioSource.Play();
            }
        }

        public void Fire(Vector3 direction)
        {
            // Check if its allowed to fire already
            if (Time.realtimeSinceStartup < timeNextBulletAllowed)
                return;

            // Check if rounds are available
            if (ammunitionEnabled && remainingRoundsInternal <= 0)
                return;

            timeNextBulletAllowed = Time.realtimeSinceStartup + fireRate;

            // Play shoot sound
            if (shootSound)
            {
                weaponAudioSource.clip = shootSound;
                weaponAudioSource.Play();
            }

            FireImplementation(weaponOrigin, direction);

            remainingRoundsInternal--;
        }

        protected abstract void FireImplementation(Transform weaponOrigin, Vector3 direction);

        /// <returns>False if at max ammunition</returns>
        public bool RestockAmmunition(int ammunition)
        {
            // If we cannot pick rounds up, skip and notify
            if (remainingRoundsInternal >= maxRounds)
                return false;

            remainingRoundsInternal += ammunition * reloadAmountMultiplier;

            // Cap the rounds to the max
            if (remainingRoundsInternal > maxRounds)
                remainingRoundsInternal = maxRounds;

            // Play reload sound
            if (reloadSound)
            {
                weaponAudioSource.clip = reloadSound;
                weaponAudioSource.Play();
            }

            // Return that rounds where picked up
            return true;
        }
    }
}
