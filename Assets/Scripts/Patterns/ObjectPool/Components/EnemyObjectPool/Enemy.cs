using System;
using System.Collections;
using Components;
using DG.Tweening;
using Patterns.ObjectPool.Interfaces;
using Patterns.Observer.Components.Components.Misc;
using ScriptableObjects;
using Scripts;
using Scripts.Patterns.State.Components;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class Enemy : AEntity, IPooleableObject, IHittable
    {
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        public IObjectPool elementPool { get; set; }
        private DamageFlash _damageFlash;
        
        private void Awake()
        {
            base.Awake();
            _isDead = false;
            _animator = GetComponent<Animator>();
            _animator.runtimeAnimatorController = entitySO.Animator;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = entitySO.Sprite;
            _damageFlash = GetComponent<DamageFlash>();
        }
        
        
        protected override void Die()
        {
            if (!_isDead)   
            {
                _isDead = true;
                _animator.SetTrigger("IsDead");
                StartCoroutine(AwaitAnimationEndAndRelease());
            }
        }
        
        IEnumerator AwaitAnimationEndAndRelease()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1);
            EventManager.Instance.EnemyKilled(transform.position);
            elementPool?.Release(this);
        }


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
        
        public IPooleableObject Clone()
        {
            GameObject clone = Instantiate(gameObject);
            Enemy enemy = clone.GetComponent<Enemy>();
            return enemy;
        }

        public void Reset()
        {
            transform.localPosition = Vector3.zero;
            _isDead = false;
        }

        public new void SetEntitySO(EntitySO entitySO)
        {
            base.entitySO = entitySO;
            _animator.runtimeAnimatorController = entitySO.Animator;
            _spriteRenderer.sprite = base.entitySO.Sprite;
            _currentLife = entitySO.Health * GameManager.Instance.CurrentWave;
        }

        public void OnHit(float damage)
        {
            _damageFlash.StartFlash();
            TakeDamage(damage);
        }
    }
}