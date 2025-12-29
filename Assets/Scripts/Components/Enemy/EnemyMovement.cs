using System;
using Patterns.ObjectPool.Components;
using ScriptableObjects;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _target;
        private Rigidbody2D _rb;
        private Enemy _enemy;
        
        float _tempDistance;
        private float _spreadFactor;

        private float _updateRate = 0.25f;
        private Vector2 _direction;
        
        private float _separationRadius = 2f; 
        private float _separationForce = 2f;  
        private Collider2D[] _neighbors = new Collider2D[10]; 

        private float _maxDistance = 20f;

        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
            _rb = GetComponent<Rigidbody2D>();
            _enemy = GetComponent<Enemy>();
            _direction = _target.position;
            _separationRadius = GetComponent<CircleCollider2D>().radius + 0.5f;
        }

        private void Update()
        { 
            if(GameManager.Instance.GetCurrentState() is not PlayingState)
                return;
            
            if(_enemy.IsDead())
                return;
            
            _tempDistance = Vector2.Distance(transform.position, _target.position); 
            
            if(_tempDistance > _maxDistance)
            {
                _direction = _target.position - transform.position;
                transform.position = (_direction * 1.75f) + (Vector2)transform.position;
                return;
            }
            
            Vector2 separation = Vector2.zero;
            int neighborCount = Physics2D.OverlapCircleNonAlloc(transform.position, _separationRadius, _neighbors);
            
            for (int i = 0; i < neighborCount; i++)
            {
                if (_neighbors[i] != null && _neighbors[i] != GetComponent<Collider2D>() && _neighbors[i].gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
                {
                    Vector2 directionToNeighbor = transform.position - _neighbors[i].transform.position;
                    float distanceToNeighbor = directionToNeighbor.magnitude;
                    if (distanceToNeighbor > 0)
                    {
                        separation += directionToNeighbor.normalized / distanceToNeighbor;
                    }
                }
            }

            if (neighborCount > 1)
            {
                separation /= (neighborCount - 1);
            }

            _updateRate -= Time.deltaTime;
            if (_updateRate <= 0)
            {
                _spreadFactor = 1 + _tempDistance / 300;
                
                _direction = new Vector2(
                    _target.position.x - (_tempDistance * _spreadFactor) / 2 + Random.Range(0, _tempDistance * _spreadFactor),
                    _target.position.y - (_tempDistance * _spreadFactor) / 2 + Random.Range(0, _tempDistance * _spreadFactor));
                _updateRate = 0.25f;
            }
            
            Vector2 movement = (_direction - (Vector2)transform.position).normalized * _enemy.GetEntitySO().Speed * Time.deltaTime * _spreadFactor;
            movement += separation * _separationForce;

            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + movement, _enemy.GetEntitySO().Speed * Time.deltaTime);
            
            Vector2 directionOrientation = (_target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionOrientation.y, directionOrientation.x) * Mathf.Rad2Deg - 90f;

            _rb.rotation = angle;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _separationRadius);
        }
    }
}