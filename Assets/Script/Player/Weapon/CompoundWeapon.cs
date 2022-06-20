using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CompoundWeaponPart
{
    public string keyBind;
    public Weapon weapon;
}

public class CompoundWeapon : MonoBehaviour
{
    public List<CompoundWeaponPart> weapons;

    public void Awake()
    {
        weapons.ForEach(part => part.weapon.Equip());
    }

    void Update()
    {
        weapons.ForEach(part =>
        {
            if (Input.GetAxisRaw(part.keyBind) > 0f)
                // TODO Maybe involve the mouse position later
                //      Then we need to call this method from the PlayerWeaponController
                // TODO The transform here should not be used in general
                part.weapon.Fire(transform.forward);
        });
    }
}