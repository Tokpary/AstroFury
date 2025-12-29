using System;
using Patterns.ObjectPool.Components;
using Patterns.Observer.Components.Components.Misc;
using Patterns.Observer.Interfaces;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using Systems.SoundSystem;
using UnityEngine;

namespace Scripts
{
    public class Player : AEntity
    {
        public float Damage { get; set; }
        public float MaxLife { get; set; }
        public float FireRate { get; set; }
        public float Speed { get; set; }
        public float Range { get; set; }    // Valor de 0 a 20
        public float Drift { get; set; }
        public float CriticalChance { get; set; }
        public float CriticalMultiplier { get; set; }

        public string CurrentWeapon { get; set; }
  
        private DamageFlash _damageFlash;
        
        private void Awake()
        {
            base.Awake();
            MaxLife = entitySO.Health;
            Damage = entitySO.Damage;
            FireRate = 10f;
            Speed = entitySO.Speed;
            Range = 2f;
            Drift = 0f;
            CriticalChance = 0f;
            CriticalMultiplier = 1f;
            _damageFlash = GetComponent<DamageFlash>();
            
        }

        private void Start()
        {
            GameManager.Instance.OnGameRestarted.AddListener(RestartPlayer);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnGameRestarted.RemoveListener(RestartPlayer);
        }

        private void RestartPlayer()
        {
            
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
                Awake();
                _currentLife = MaxLife;
                GetComponent<PlayerStatsManager>().NotifyObservers();
            
        }
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if(_currentLife > MaxLife)
                _currentLife = MaxLife;
            
            SoundManager.Instance.PlaySfx("DamageSFX");
            _damageFlash.StartFlash();
            GetComponent<PlayerStatsManager>().NotifyObservers();
        }

        public float GetCurrentLife()
        {
            return _currentLife;
        }
        
        protected override void Die()
        {
            base.Die();
            GameManager.Instance.EndGame();
        }

    }
}