using System;
using Patterns.ObjectPool.Components;
using ScriptableObjects;
using Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Components.Weapons
{
    public abstract class AWeapon : MonoBehaviour, IWeapon
    {
        protected ProjectileManager _projectileManager;
        protected bool isShooting = false;
        protected Player _player;
        [SerializeField] public UpgradeItemSO weapon;

        protected virtual void Awake()
        {
            _player = FindObjectOfType<Player>().GetComponent<Player>();
            _projectileManager = GetComponent<ProjectileManager>();
        }

        protected virtual void Update()
        {
            if(!isShooting)
                return;
            
            
        }

        public virtual void StartShooting()
        {
            isShooting = true;
        }
        
        public virtual void EndShooting()
        {
            isShooting = false;
        }
        
        public void Shoot()
        {
            _projectileManager.ShootProjectile();
        }
        
        public void IsShooting(bool value)
        {
            isShooting = value;
        }
    }
}