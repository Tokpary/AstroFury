using System;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using Patterns.Observer.Components;
using Patterns.State.Interfaces;
using ScriptableObjects;
using Scripts.Patterns.State.States;
using Systems.SoundSystem;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using PauseState = Scripts.Patterns.State.States.PauseState;

namespace Scripts.Patterns.State.Components
{
    public class GameManager : Singleton<GameManager>, IGameState
    {
        private IState _currentState;
        private IState _previousState;
        public int CurrentWave { get; set; }
        public readonly int LastWave = 5; 
        [SerializeField] public TMP_Text Timer;
        
        [SerializeField] public List<UpgradeItemSO> upgradeItems;
        [SerializeField] public List<UpgradeItemSO> shopItems;
        [SerializeField] public List<UpgradeItemSO> weaponItems;
        
        public event Action<IState> OnStateChanged;
        public UnityEvent<UpgradeItemSO> OnUpgradeAcquired; 
        public UnityEvent<string> OnWeaponAcquired; 
        public UnityEvent OnGameRestarted; 
        private void Awake()
        {
            base.Awake();
            CurrentWave = 1;
            _currentState = null;
            _previousState = null;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "GameLevel")
            {
                StartGame();
            }
        }

        public void ContinueGame()
        {
            SetState(new UpgradePlayerState(this));
        }


        public void StartGame()
        {
            CurrentWave = 1;
            OnGameRestarted?.Invoke();
            SetState(new CountdownState(this));
        }
        
        
        public void PauseGame()
        {
            if (_currentState is PauseState)
            {
                ReturnFromPause();
                return;
            }
            SoundManager.Instance.TogglePauseMusic();
            SoundManager.Instance.PlaySfx("UISkipSFX");
            SetState(new PauseState(this));
        }

        public void ReturnFromPause()
        {
            SoundManager.Instance.PlaySfx("UIConfirmSFX");
            SoundManager.Instance.TogglePauseMusic();
            _currentState.Exit(this);
            (_currentState, _previousState) = (_previousState, _currentState);
            OnStateChanged?.Invoke(_currentState);
        }
        
        public void ReturnMainMenu()
        {
            SoundManager.Instance.PlaySfx("UIConfirmSFX");
            CurrentWave = 1;
            _currentState.Exit(this);
            _currentState = null;
            _previousState = null;
            SceneManager.LoadScene("MainMenu");
        }

        public void EndGame()
        {
            SetState(new GameOverState(this));
        }

        public void Update()
        {
            //Debug.Log("Current state: " + _currentState.GetType().Name);
            //Debug.Log("Current state: " + _previousState.GetType().Name);
            if(_currentState != null)
                _currentState.Update();
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }

        public void SetState(IState state)
        {
            if (state is PauseState)
            {
                _previousState = _currentState;
                _currentState = state;
                _currentState.Enter(this);
                OnStateChanged?.Invoke(_currentState);
                return;
            }
            
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }
            _currentState = state;
            _currentState.Enter(this);
            OnStateChanged?.Invoke(_currentState);
            
            
            
            if (state is PauseState && _currentState is CountdownState)
            {
                Debug.Log("Cannot pause during countdown");
                return;
            }
        }
        
        

        public void SelectUpgrade(int upgradeIndex)
        {
            Assert.IsTrue(_currentState is ISelectableUpgradeState, "Current state is not ISelectableUpgradeState");
            ((ISelectableUpgradeState) _currentState).SelectUpgrade(upgradeIndex);
        }

        public void SkipUpgrade()
        {
            Assert.IsTrue(_currentState is ISelectableUpgradeState, "Current state is not ISelectableUpgradeState");
            SoundManager.Instance.PlaySfx("UISkipSFX");
            ((ISelectableUpgradeState) _currentState).SkipUpgrade();
        }

        public void AcceptUpgrade()
        {
            Assert.IsTrue(_currentState is ISelectableUpgradeState, "Current state is not ISelectableUpgradeState");
            ((ISelectableUpgradeState) _currentState).AcceptUpgrade(this);
        }

        public void DisplayUpgrades()
        {
            Assert.IsTrue(_currentState is ISelectableUpgradeState, "Current state is not ISelectableUpgradeState");
            ((ISelectableUpgradeState) _currentState).DisplayUpgrades(this);
        }
        
        public float GetScore()
        {
            return GameObject.Find("Player").GetComponentInChildren<CurrencyManager>().Currency;
        }
    }
}