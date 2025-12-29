using System.Collections.Generic;
using System.Linq;
using Patterns.State.Interfaces;
using ScriptableObjects;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Patterns.State.States
{
    public class UpgradePlayerState : AGameState, ISelectableUpgradeState
    {
        private GameObject canvas;
        private GameObject _upgradeUI;
        private TMP_Text _countdownText;
        private SlotManager _slotManager;
        private float _countdownTimer = 0;
        
        public UpgradePlayerState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(GameManager gameManager)
        {
            SoundManager.Instance.PlayMusic("UpgradeStateMusic");
            canvas = GameObject.Find("Canvas").gameObject;
            _upgradeUI = canvas.transform.Find("UpgradeStatsUI").gameObject;
            _slotManager = _upgradeUI.gameObject.GetComponentInChildren<SlotManager>();
            _upgradeUI.SetActive(true);
            _gameState.DisplayUpgrades();
        }

        public override void Exit(GameManager gameManager)
        {
            _slotManager.ClearSlots();
            _upgradeUI.SetActive(false);
        }

        public override void Update()
        {
        }

        public void SkipUpgrade()
        {
            _gameState.SetState(new ShoppingState(_gameState));
        }

        public void SelectUpgrade(int upgradeIndex)
        {
            
        }


        public void AcceptUpgrade(GameManager gameManager)
        {
            var currentUpgradeItem = _slotManager.GetCurrentUpgradeItem();
            
            if(currentUpgradeItem != null)
            {
                SoundManager.Instance.PlaySfx("UIConfirmSFX");
                gameManager.OnUpgradeAcquired?.Invoke(currentUpgradeItem);
                _gameState.SetState(new ShoppingState(_gameState));
            }
            else
            {
                SoundManager.Instance.PlaySfx("UIErrorSFX");
                _slotManager.DisplayNotSelectedError();
            }
        }

        public void DisplayUpgrades(GameManager gameManager)
        {
            
            List<UpgradeItemSO> selectedUpgrades = gameManager.upgradeItems.OrderBy(x => Random.value).Take(3).ToList();
            
            _slotManager.LoadSlots(selectedUpgrades);
        }
    }
}