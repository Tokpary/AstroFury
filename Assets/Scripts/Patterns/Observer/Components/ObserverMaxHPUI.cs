using System;
using Scripts;
using TMPro;
using UnityEngine;

namespace Patterns.Observer.Components
{
    public class ObserverMaxHPUI : MonoBehaviour, Interfaces.IObserver<Player>
    {
        private TMP_Text _maxHPText;
        private void Awake()
        {
            _maxHPText = GetComponent<TMP_Text>();
            GameObject player = GameObject.FindWithTag("Player");
            PlayerStatsManager playerStats = player.GetComponent<PlayerStatsManager>();
            playerStats.AddObserver(this);
        }

        private void Start()
        {
            _maxHPText.text = FindObjectOfType<Player>().MaxLife.ToString();
        }

        public void UpdateObserver(Player data)
        {
            _maxHPText.text = $"{data.MaxLife}";
        }
    }
}