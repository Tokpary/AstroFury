using System;
using Components;
using Systems.SoundSystem;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class ExplosiveProjectile : AProjectile
    {
        private int _explosionDamage = 10;
        private Collider2D[] _affectedEnemies = new Collider2D[20];
        private void OnEnable()
        {
            transform.localScale = Vector3.one * 4f;
            _speed = 5f;
            
        }    
        protected override void OnHit()
        {
            CreateExplosion();
            elementPool?.Release(this);
           // base.OnHit();
        }
        protected override void PlayHitSound()
        {
            SoundManager.Instance.PlaySfx("RocketSFX");
        }
        private void CreateExplosion()
        {
            
            int hitedEnemies = Physics2D.OverlapCircleNonAlloc(transform.position,_player.Range/2f, _affectedEnemies);
            
            EventManager.Instance.CreateExplosion(_explosionColor, transform.position);
            
            for (int i = 0; i < hitedEnemies; i++)
            {
                DealExplosionDamage(_affectedEnemies[i]);
            }
        }
        
        protected virtual void DealExplosionDamage(Collider2D other)
        {
            IHittable hittable = other.GetComponent<IHittable>();
            if(hittable != null) 
            {
                if (other.GetComponent<Enemy>().IsDead())
                {
                    return;
                }
                
                float damage = _player.Damage;
                hittable.OnHit(damage);
                EventManager.Instance.DamageDealt(damage, other.transform.position);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }
    }
}