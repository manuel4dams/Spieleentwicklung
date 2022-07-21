using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptGG
{
    [Serializable]
    public struct CompoundWeaponPart
    {
        public string keyBind;
        public WeaponGG weapon;
        public Slider ammunitionSlider;
        public Image ammunitionImage;
    }

    [Serializable]
    public class CompoundWeapon
    {
        public Sprite weaponSprite;
        [NonReorderable] // Because unity sucks in drawing their UI, disabling reorder fixes a failure where the first entry overlaps some content
        public List<CompoundWeaponPart> weapons;

        public bool active
        {
            set => Activate(value);
        }

        public void Activate(bool active)
        {
            weapons.ForEach(part =>
            {
                // Enable or disable visual container
                part.weapon.weaponVisualContainers.ForEach(go => go.SetActive(active));

                // Enable or disable sliders
                part.ammunitionSlider.gameObject.SetActive(active && part.weapon.ammunitionEnabled);

                // Let the weapon handle its equip
                part.weapon.Equip();
            });
        }

        public void UpdateAmmunitionSlider()
        {
            weapons.ForEach(part =>
            {
                if (part.weapon.ammunitionEnabled)
                {
                    part.ammunitionSlider.maxValue = part.weapon.maxRounds;
                    part.ammunitionSlider.value = part.weapon.remainingRounds;
                }
            });
        }
    }
}
