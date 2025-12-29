using DG.Tweening;
using Patterns.State.Interfaces;
using Scripts.Patterns.State.Components;
using TMPro;
using UnityEngine;

namespace Scripts.Patterns.State.States
{
    public class PauseState : AGameState
    {
        private GameObject canvas;
        private GameObject _countdownPanel;
        private GameObject _pauseUI;
        
        public PauseState(IGameState gameState) : base(gameState)
        {
        }
        
        public override void Enter(GameManager gameManager)
        {
            canvas = GameObject.Find("Canvas").gameObject;
            _pauseUI = canvas.transform.Find("PauseUI").gameObject;
            _pauseUI.SetActive(true);
        }

        public override void Exit(GameManager gameManager)
        {
            _pauseUI.SetActive(false);
        }

        public override void Update()
        {
        }
    }
}