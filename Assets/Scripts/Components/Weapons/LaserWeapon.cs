using System;
using UnityEngine;

namespace Components.Weapons
{
    public class LaserWeapon : AWeapon
    {

        public override void StartShooting()
        {
            base.StartShooting();
            Shoot();
        }
        
        public override void EndShooting()
        {
            base.StartShooting();
            _projectileManager.ClearProjectiles();
        }
    }
}