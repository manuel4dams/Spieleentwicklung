using UnityEngine;

namespace ScriptGG
{
    public class MeleeWeapon : ProjectileWeaponGG
    {
        [Header("Melee settings")] //
        public Animator animator;
        public string meleeTrigger;

        protected override void FireImplementation(Transform weaponOrigin, Vector3 direction)
        {
            animator.SetTrigger(meleeTrigger);
            base.FireImplementation(weaponOrigin, direction);
        }
    }
}
