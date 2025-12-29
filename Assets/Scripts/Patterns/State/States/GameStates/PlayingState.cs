using Patterns.ObjectPool.Components;
using Patterns.State.Interfaces;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using UnityEngine;

namespace Scripts.Patterns.State.States
{
    public class PlayingState : AGameState
    {
        float _time = 0;
        float _waveTime = 30;
        
        public PlayingState(IGameState gameState) : base(gameState)
        {
        }

        
        
        public override void Enter(GameManager gameManager)
        {
            _time = 0;
            _waveTime = 30;
        }

        public override void Exit(GameManager gameManager)
        {
            Debug.Log("Exiting Playing State");
            GameManager.Instance.CurrentWave++;
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            GameManager.Instance.Timer.text = string.Format("{00:00}", (_waveTime - _time));
            if (_time >= _waveTime)
            {
                if (GameManager.Instance.CurrentWave % 3 == 0)
                {
                    SoundManager.Instance.PlaySfx("UISkipSFX");
                    _gameState.SetState(new UpgradeWeaponState(_gameState));
                } 
                else if (GameManager.Instance.CurrentWave == GameManager.Instance.LastWave)
                {
                    _gameState.SetState(new GameEndState(_gameState));
                }
                else
                {
                    SoundManager.Instance.PlaySfx("UISkipSFX");
                    _gameState.SetState(new UpgradePlayerState(_gameState));
                }
            }
        }
    }
}