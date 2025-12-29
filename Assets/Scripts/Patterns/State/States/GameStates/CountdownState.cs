using DG.Tweening;
using Patterns.State.Interfaces;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using TMPro;
using UnityEngine;

namespace Scripts.Patterns.State.States
{
    public class CountdownState : AGameState
    {
        private GameObject canvas;
        private GameObject _countdownPanel;
        private TMP_Text _countdownText;
        private float _countdownTimer = 0;
        
        public CountdownState(IGameState gameState) : base(gameState)
        {
        }
        
        public override void Enter(GameManager gameManager)
        {
            SoundManager.Instance.PlayMusic("MenuMusic");
            canvas = GameObject.Find("Canvas").gameObject;
            _countdownPanel = canvas.transform.Find("CountdownUI").gameObject;
            _countdownPanel.SetActive(true);
            _countdownText = GameObject.Find("LblCountdown").GetComponent<TMP_Text>();
            _countdownText.transform.localScale = new Vector3(1, 1, 1);
            _countdownText.text = $"Wave {gameManager.CurrentWave}";
            Sequence countdownSequence = DOTween.Sequence();
            countdownSequence
                .Append(_countdownText.DOFade(1, 0.5f))
                .Append(_countdownText.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f))
                .AppendInterval(1.5f)
                .AppendCallback(() => _countdownText.text = "3")
                .Append(_countdownText.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 1f, 1, 0.5f))
                .AppendCallback(() => _countdownText.text = "2")
                .Append(_countdownText.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 1f, 1, 0.5f))
                .AppendCallback(() => _countdownText.text = "1")
                .Append(_countdownText.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 1f, 1, 0.5f))
                .AppendCallback(() => _countdownText.text = "GO!")
                .Append(_countdownText.transform.DOPunchScale(new Vector3(1.5f, 1.5f, 1.5f), 1f, 1, 0.5f))
                .OnComplete(() => _gameState.SetState(new PlayingState(_gameState)));
            DOTween.Play(countdownSequence);
        }

        public override void Exit(GameManager gameManager)
        {
            _countdownPanel.SetActive(false);
        }

        public override void Update()
        {
        }
    }
}