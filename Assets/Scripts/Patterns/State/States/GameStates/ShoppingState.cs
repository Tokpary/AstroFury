using System.Collections.Generic;
using System.Linq;
using Patterns.Observer.Components;
using Patterns.State.Interfaces;
using ScriptableObjects;
using Scripts.Patterns.State.Components;
using Systems.SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Patterns.State.States
{
    public class ShoppingState : AGameState, ISelectableUpgradeState
    {
        private GameObject canvas;
        private GameObject _upgradeUI;
        private TMP_Text _countdownText;
        private SlotManager _slotManager;
        private float _countdownTimer = 0;
        
        public ShoppingState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(GameManager gameManager)
        {
            SoundManager.Instance.PlayMusic("UpgradeStateMusic");
            canvas = GameObject.Find("Canvas").gameObject;
            _upgradeUI = canvas.transform.Find("UpgradeStatsUI").gameObject;
            _slotManager = _upgradeUI.gameObject.GetComponentInChildren<SlotManager>();
            _gameState.DisplayUpgrades();
            _upgradeUI.SetActive(true);
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
            _gameState.SetState(new CountdownState(_gameState));
        }

        public void SelectUpgrade(int upgradeIndex)
        {
            
            //gameManager.OnUpgradeAcquired?.Invoke(currentUpgradeItem);
        }

        public void AcceptUpgrade(GameManager gameManager)
        {
            var currentUpgradeItem = _slotManager.GetCurrentUpgradeItem();
            var currency = GameObject.FindWithTag("Player").GetComponentInChildren<CurrencyManager>();
            if(currentUpgradeItem != null)
            {
                if(currency.Currency < currentUpgradeItem.Price)
                {
                    SoundManager.Instance.PlaySfx("UIErrorSFX");
                    _slotManager.DisplayNotEnoughCurrencyError();
                }
                else
                {
                    SoundManager.Instance.PlaySfx("UIConfirmSFX");
                    currency.Currency -= currentUpgradeItem.Price;
                    currency.NotifyObservers();
                    gameManager.OnUpgradeAcquired?.Invoke(currentUpgradeItem);
                    _gameState.SetState(new CountdownState(_gameState));
                }
            }
            else
            {
                SoundManager.Instance.PlaySfx("UIErrorSFX");
                _slotManager.DisplayNotSelectedError();
            }
        }

        public void DisplayUpgrades(GameManager gameManager)
        {
            List<UpgradeItemSO> selectedUpgrades = gameManager.shopItems.OrderBy(x => Random.value).Take(3).ToList();
            _slotManager.LoadSlots(selectedUpgrades);
        }
    }
}