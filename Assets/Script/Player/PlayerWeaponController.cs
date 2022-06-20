using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapons")] //
    public List<CompoundWeapon> weapons;

    // Variables
    [ReadOnly] [SerializeField] private int equippedWeaponIndex;

    // Properties
    public CompoundWeapon CurrentWeapon => weapons[equippedWeaponIndex];

    void Start()
    {
        weapons.ForEach(weapon => weapon.gameObject.SetActive(false));
        CurrentWeapon.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            CurrentWeapon.gameObject.SetActive(false);
            equippedWeaponIndex = (equippedWeaponIndex + 1) % weapons.Count;
            CurrentWeapon.gameObject.SetActive(true);
        }
    }

    public bool RestockAmmunition(int ammunitionAmount)
    {
        var restocked = false;
        foreach (var compoundWeapon in weapons)
            foreach (var weaponPart in compoundWeapon.weapons)
                restocked |= weaponPart.weapon.RestockAmmunition(ammunitionAmount);
        return restocked;
    }
}
