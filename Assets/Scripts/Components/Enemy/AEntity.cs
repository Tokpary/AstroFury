using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using ScriptableObjects;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using UnityEngine;

namespace Scripts
{
    public abstract class AEntity : MonoBehaviour
    {
        [SerializeField] protected EntitySO entitySO;
        protected float _currentLife;
        protected bool _isDead;

        protected void Awake()
        {
            _currentLife = entitySO.Health;
        }
    
        
        public virtual void TakeDamage(float damage)
        {
            _currentLife -= damage;
            if (_currentLife <= 0)
            {
                Die();
            }
        }
    
        public void SetEntitySO(EntitySO entitySO)
        {
            this.entitySO = entitySO;
        }

        public bool IsDead()
        {
            return _isDead;
        }
        
        public EntitySO GetEntitySO()
        {
            return entitySO;
        }
        protected virtual void Die()
        {
        }

        
    }

}
