using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Patterns.ObjectPool.Interfaces;
using Scripts;
using Systems.SoundSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public abstract class AProjectile : MonoBehaviour, IPooleableObject
    {
        protected Player _player;
        protected float _speed = 10f;
        protected float _range;
        [SerializeField] protected float _damage;
        [SerializeField] protected ParticleSystem.MinMaxGradient _explosionColor;
        public bool Active
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }
        public IObjectPool elementPool { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DealDamage(other);
        }

        protected virtual void DealDamage(Collider2D other)
        {
            IHittable hittable = other.GetComponent<IHittable>();
            if(hittable != null) 
            {
                if (other.GetComponent<Enemy>().IsDead())
                {
                    return;
                }
                
                float damage = _player.Damage * _damage;
                if(_player.CriticalChance >= UnityEngine.Random.Range(0f, 100f))
                {
                    damage *= _player.CriticalMultiplier;
                }
                hittable.OnHit(damage);
                PlayHitSound();
                EventManager.Instance.CreateExplosion(_explosionColor, other.gameObject.transform.position);
                EventManager.Instance.DamageDealt(damage, other.transform.position);
                OnHit();
            }
        }
        
        protected virtual void PlayHitSound()
        {
            SoundManager.Instance.PlaySfx("MissileSFX");
        }

        protected virtual void Awake()
        {
            _player = FindObjectOfType<Player>();
            _range = _player.Range / 4f;
        }
        protected virtual void Update()
        {
            transform.Translate(Vector2.up * ((_speed + _player.Speed) * Time.deltaTime));
        
            _range -= Time.deltaTime;
            if (_range <= 0)
            {
                elementPool?.Release(this);
            }
        }

        protected virtual void OnHit()
        {
            elementPool?.Release(this);
        }
    

        public IPooleableObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            AProjectile missileProjectile = clone.GetComponent<AProjectile>();
            return missileProjectile;
        }

        public void Reset()
        {
            _range = _player.Range;
            _range = _player.Range / 4f;
        }
    }
}