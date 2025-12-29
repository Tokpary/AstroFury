using System;
using Components;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class LaserProjectile : AProjectile
    { 
        private float _timer;
        [SerializeField] private float _tickLimitator;
        public float fireSpeedFactor = 0.2f;
        protected override void Update()
        {
            transform.position = _player.transform.position;
            transform.rotation = _player.transform.rotation;
        }

        protected void OnEnable()
        {
            transform.localScale = new Vector3(2f, _player.Range * 2, 2f);
            _timer = GetFireRateTime(_player.FireRate, fireSpeedFactor);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            { 
                DealDamage(other);
                _timer = GetFireRateTime(_player.FireRate, fireSpeedFactor);
            }
            
        }
        
        private float GetFireRateTime(float fireRate, float factor)
        {
            float normalizedFireRate = Mathf.Clamp01(fireRate / 100f);
            float invertedFireRate = 1f - normalizedFireRate;
            return invertedFireRate * factor;
        }

        protected override void OnHit()
        {
            
        }
    }
}