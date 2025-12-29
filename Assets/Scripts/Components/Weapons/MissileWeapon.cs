using UnityEngine;

namespace Components.Weapons
{
    public class MissileWeapon : AWeapon
    {
        float _timer = 0;
        public float fireSpeedFactor = 0.5f;
        
        protected override void Awake()
        {
            base.Awake();
            _timer = GetFireRateTime(_player.FireRate, fireSpeedFactor);
        }
        
        protected override void Update()
        {
            
            _timer -= Time.deltaTime;
            if (_timer <= 0 && isShooting)
            { 
                Shoot(); 
                _timer = GetFireRateTime(_player.FireRate, fireSpeedFactor);
            }
            
        }
        
        private float GetFireRateTime(float fireRate, float factor)
        {
            float normalizedFireRate = Mathf.Clamp01(fireRate / 100f);
            float invertedFireRate = 1f - normalizedFireRate;
            return (invertedFireRate * factor) + 0.05f;
        }

        
    }
}