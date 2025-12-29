using DG.Tweening;
using Patterns.State.Interfaces;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using TMPro;
using UnityEngine;

namespace Scripts.Patterns.State.States
{
    public class GameOverState : AGameState
    {
        private GameObject canvas;
        private GameObject _countdownPanel;
        private TMP_Text _countdownText;
        private float _countdownTimer = 0;
        private GameObject _gameOverPanel;
        private GameObject _buttonsPanel;
        private GameObject _scoreText;
        
        public GameOverState(IGameState gameState) : base(gameState)
        {
        }
        
        public override void Enter(GameManager gameManager)
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.PlaySfx("GameOverSFX");
            canvas = GameObject.Find("Canvas").gameObject;
            _countdownPanel = canvas.transform.Find("CountdownUI").gameObject;
            
            _countdownText = _countdownPanel.transform.Find("LblCountdown").GetComponent<TMP_Text>();
            _countdownText.text = $"GAME OVER";
            _countdownText.transform.localScale = Vector3.zero;
            
            _gameOverPanel = _countdownPanel.transform.Find("GameOverLayout").gameObject;
            
            _gameOverPanel.SetActive(true);
            
            _scoreText = _gameOverPanel.transform.Find("ScoreText").gameObject;
            _scoreText.GetComponent<TMP_Text>().text = $"Final Score: {gameManager.GetScore()}";
            _scoreText.transform.localScale = Vector3.zero;
            
            _buttonsPanel = _gameOverPanel.transform.Find("ButtonPanel").gameObject;
            _buttonsPanel.SetActive(false);
            _countdownText.color = Color.red;
            
            _countdownPanel.SetActive(true);
            
            Sequence countdownSequence = DOTween.Sequence();
            countdownSequence
                .Append(_countdownText.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutExpo))
                .AppendInterval(1.5f)
                .Append(_scoreText.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutExpo))
                .AppendInterval(1.5f)
                .OnComplete(() => _buttonsPanel.SetActive(true));
            DOTween.Play(countdownSequence);
        }

        public override void Exit(GameManager gameManager)
        {
            _countdownText.color = Color.white;
            _countdownPanel.SetActive(false);
            _gameOverPanel.SetActive(false);
        }

        public override void Update()
        {
        }
    }
}