using System;
using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class InputDetector : IUpdateHook
    {
        // Output
        public float horizontal { get; private set; }
        public bool sneaking { get; private set; }
        public bool jumping { get; private set; }
        public bool swapWeapon { get; private set; }
        public int weaponIndex { get; private set; }
        public event Action swapWeaponPressed;

        [ExcludeFromGraph]
        public void Update()
        {
            if (Time.timeScale == 0f)
            {
                horizontal = 0f;
                sneaking = false;
                jumping = false;
                swapWeapon = false;
                weaponIndex = -1;
                return;
            }

            horizontal = Input.GetAxis("Horizontal");
            sneaking = Input.GetButton("Fire3");
            jumping = Input.GetButton("Jump");
            swapWeapon = Input.GetButtonDown("Submit");
            if (swapWeapon)
                swapWeaponPressed?.Invoke();

            SetPressedNumber();
        }

        public void SetPressedNumber()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponIndex = 0;
                swapWeaponPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponIndex = 1;
                swapWeaponPressed?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                weaponIndex = 2;
                swapWeaponPressed?.Invoke();
            }

            weaponIndex = -1;
        }
    }
}
