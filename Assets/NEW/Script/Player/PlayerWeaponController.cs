using GameGraph;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerWeaponController
    {
        // References
        public PlayerState playerState;
        public Image weaponImage;

        // Input
        public int restockAmmunitionAmount { private get; set; }

        // Output
        public int equippedWeaponIndex { get; private set; }
        public CompoundWeapon currentWeapon => playerState.weapons[equippedWeaponIndex];
        public bool restockHappened { get; private set; }

        public void Init()
        {
            playerState.weapons.ForEach(weapon => weapon.active = false);
            currentWeapon.active = true;
        }

        public void SwapWeapon()
        {
            // Swap the weapon
            currentWeapon.active = false;
            equippedWeaponIndex = (equippedWeaponIndex + 1) % playerState.weapons.Count;
            currentWeapon.active = true;

            // Update the icon
            weaponImage.sprite = currentWeapon.weaponSprite;
        }

        public void RestockAmmunition()
        {
            var restocked = false;
            foreach (var compoundWeapon in playerState.weapons)
            {
                foreach (var weaponPart in compoundWeapon.weapons)
                    restocked |= weaponPart.weapon.RestockAmmunition(restockAmmunitionAmount);
            }
            restockHappened = restocked;
        }
    }
}
