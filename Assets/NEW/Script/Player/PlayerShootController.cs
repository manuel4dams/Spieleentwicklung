using GameGraph;
using JetBrains.Annotations;
using UnityEngine;

namespace ScriptGG
{
    [GameGraph]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class PlayerShootController
    {
        // Input
        public CompoundWeapon currentWeapon { private get; set; }
        public Vector3 shootDirection { private get; set; }

        public void DetectShootButton()
        {
            currentWeapon.weapons.ForEach(part =>
            {
                if (Time.timeScale == 0f)
                    return;

                if (Input.GetAxisRaw(part.keyBind) > 0f)
                    // TODO Maybe involve the mouse position later
                    //      Then we need to call this method from the PlayerWeaponController
                    // TODO The transform here should not be used in general
                    part.weapon.Fire(shootDirection);
            });
        }
    }
}
