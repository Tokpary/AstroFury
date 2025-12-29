using System;
using Components;
using DG.Tweening;
using UnityEngine;

namespace Patterns.ObjectPool.Components
{
    public class OrbProjectile : AProjectile
    {
        private float _tickRate;
        [SerializeField] private float _tickLimitator;
        private void OnEnable()
        {
            transform.localScale = Vector3.one * 8f;
            _range = _player.Range / 2f;
            _speed = 10f;
        }

        protected override void Update()
        {
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
            _range -= Time.deltaTime;
            
            if (_speed >= 0.2f)
            {
                _speed -= 0.01f;
            }
            
            if (_range <= 0)
            {
                elementPool?.Release(this);
            }
        }
        

        protected override void OnHit()
        {
            ResizeOrb();
        }
        
        private void ResizeOrb()
        {
            transform.DOScale(transform.localScale - 0.4f * Vector3.one, 0.1f);
            if(transform.localScale.x <= 0.1f)
            {
                transform.localScale = Vector3.one;
                elementPool?.Release(this);
            }
        }
        
        
    }
}