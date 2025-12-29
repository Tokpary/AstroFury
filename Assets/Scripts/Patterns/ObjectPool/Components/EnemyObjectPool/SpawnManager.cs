using System;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using Patterns.ObjectPool;
using Patterns.ObjectPool.Interfaces;
using Patterns.State.Interfaces;
using ScriptableObjects;
using Scripts.Patterns.State.Components;
using Scripts.Patterns.State.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Patterns.ObjectPool.Components
{
    public class SpawnManager : AObjectPoolManager<Enemy>
    {
        
        [SerializeField] private List<EntitySO> _enemiesList;
        [SerializeField] private List<EntitySO> _currentEnemiesList;
         
        private float _timer = 0f; 
        [SerializeField] private float _spawnRadius = 15f;
        
        [SerializeField] private float _maxSpawnInterval = 3f;
        [SerializeField] private float _minSpawnInterval = 3f;
        
        [SerializeField] private int _spawnGroupAmount = 1;
        
        
        private Transform _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable()
        {
            Debug.Log(GameManager.Instance);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += OnStateChanged;
            }

            _timer = UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= OnStateChanged;
            }
        }
        private void Update()
        {
            if (spawning)
            {
                _timer -= Time.deltaTime; 
                
                if (_timer <= 0f) 
                {
                    for (int i = 0; i < _spawnGroupAmount; i++)
                        CreateEnemy(); 
                    _timer = UnityEngine.Random.Range(_minSpawnInterval, _maxSpawnInterval);
                }
            } 
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = CreatePoolElement((Enemy) elementPrototype, initialNumberOfElements, allowAddNewElements);

            if (enemy)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                enemy.transform.position = spawnPosition;
                
                enemy.SetEntitySO(_currentEnemiesList[UnityEngine.Random.Range(0, _currentEnemiesList.Count)]);
            }
            
            return enemy;
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

            float x = Mathf.Cos(angle) * _spawnRadius;
            float y = Mathf.Sin(angle) * _spawnRadius;

            return new Vector3(_player.position.x + x, _player.position.y + y, 0f);
        }
        
        private void SetSpawnIntervals(float minInterval, float maxInterval)
        {
            _minSpawnInterval = minInterval;
            _maxSpawnInterval = maxInterval;
        }
        
        public void StartSpawning()
        {
            spawning = true;
        }

        public void StopSpawning()
        {
            spawning = false;
        }
        
        void OnStateChanged(IState _currentState)
        {
            if (_currentState is PlayingState)
            {
                SetEnemiesDifficulty(GameManager.Instance.CurrentWave);
                StartSpawning();
            }
            else
            {
                StopSpawning();
            }
        }
        
        public void SetEnemiesDifficulty(int waveIdx)
        {
            _currentEnemiesList.Clear();
            switch (waveIdx)
            {
                case 0:
                    SetSpawnIntervals(3f, 4f);
                    _spawnGroupAmount = 1;
                    _currentEnemiesList.Add(_enemiesList[1]);
                    break;
                case 1:
                    _spawnGroupAmount = 2;
                    _currentEnemiesList.Add(_enemiesList[1]);
                    break;
                case 2:
                    _currentEnemiesList.Add(_enemiesList[1]);
                    _currentEnemiesList.Add(_enemiesList[2]);
                    break;
                case 3:
                    SetSpawnIntervals(2f, 3f);
                    _currentEnemiesList.Add(_enemiesList[1]);
                    _currentEnemiesList.Add(_enemiesList[2]);
                    break;
                case 4:
                    _spawnGroupAmount = 3;
                    _currentEnemiesList.Add(_enemiesList[1]);
                    _currentEnemiesList.Add(_enemiesList[2]);
                    break;
                case 5:
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 6:
                    SetSpawnIntervals(1f, 2f);
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 7:
                    _spawnGroupAmount = 5;
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 8:
                    SetSpawnIntervals(0.5f, 1.5f);
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 9:
                    _spawnGroupAmount = 7;
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 10:
                    SetSpawnIntervals(0.25f, 0.75f);
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 11:
                    _spawnGroupAmount = 10;
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                case 12:
                    SetSpawnIntervals(0.25f, 0.5f);
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
                default:
                    _spawnGroupAmount++;
                    _currentEnemiesList.AddRange(_enemiesList);
                    break;
            }
        }
    }
}