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
        public bool shooting { get; private set; }
        public bool swapWeapon { get; private set; }
        public event Action swapWeaponPressed;

        [ExcludeFromGraph]
        public void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            sneaking = Input.GetButton("Fire3");
            jumping = Input.GetButton("Jump");
            shooting = Input.GetButton("Fire1");
            swapWeapon = Input.GetButtonDown("Submit");
            if(swapWeapon)
                swapWeaponPressed?.Invoke();
        }
    }
}
