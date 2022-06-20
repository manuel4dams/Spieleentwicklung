using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] weapons;
    private bool[] weaponAvailable;
    public Image weaponImage;

    private int currentActiveWeapon;

    // Start is called before the first frame update
    void Start()
    {
        weaponAvailable = new bool[weapons.Length];
        for (var i = 0; i < weapons.Length; i++) weaponAvailable[i] = false;
        DeactivateWeapons();
        for (var i = 0; i < weapons.Length; i++) weaponAvailable[i] = true;
        currentActiveWeapon = 0;
        SeWeaponActive(currentActiveWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            int i;
            for (i = currentActiveWeapon + 1; i < weapons.Length; i++)
            {
                if (weaponAvailable[i])
                {
                    currentActiveWeapon = i;
                    SeWeaponActive(currentActiveWeapon);
                    return;
                }
            }
            for (i = 0; i < currentActiveWeapon; i++)
            {
                if (weaponAvailable[i])
                {
                    currentActiveWeapon = i;
                    SeWeaponActive(currentActiveWeapon);
                    return;
                }
            }
        }
    }

    public void SeWeaponActive(int weapon)
    {
        if (!weaponAvailable[weapon]) return;
        DeactivateWeapons();
        weapons[weapon].SetActive(true);
        weapons[weapon].GetComponentInChildren<FireProjectile>().InitWeapon();
    }

    void DeactivateWeapons()
    {
        foreach (var weapon in weapons)
            weapon.SetActive(false);
    }

    void ActivateWeapons(int weapon)
    {
        weaponAvailable[weapon] = true;
    }
}
