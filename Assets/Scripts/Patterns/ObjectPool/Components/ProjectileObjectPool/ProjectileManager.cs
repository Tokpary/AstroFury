using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class ProjectileManager : AObjectPoolManager<AProjectile>
    {
        private Transform _shipTransform;
        
        private new void Start()
        {   
            base.Start();
            _shipTransform = GameObject.Find("Player").transform;
        }

        public void ShootProjectile()
        {
            AProjectile projectile = CreateProjectile();
        }
    
        private AProjectile CreateProjectile()
        {
            AProjectile projectile = CreatePoolElement(elementPrototype, initialNumberOfElements, allowAddNewElements);
            
            if (projectile)
            {
                projectile.transform.localPosition = _shipTransform.GetChild(0).position;
                projectile.transform.rotation = _shipTransform.rotation;
            }
            return projectile;
        }
        
        public void ClearProjectiles()
        {
            ReleaseAllElements();
        }
    }
}

