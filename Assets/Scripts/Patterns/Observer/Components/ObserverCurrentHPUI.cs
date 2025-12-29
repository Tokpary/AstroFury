using Patterns.Observer.Interfaces;
using Scripts;
using TMPro;
using UnityEngine;

namespace Patterns.Observer.Components
{
    public class ObserverCurrentHPUI : MonoBehaviour, IObserver<Player>
    {
        private TMP_Text _currentHPText;
        private void Awake()
        {
            _currentHPText = GetComponent<TMP_Text>();
            GameObject player = GameObject.FindWithTag("Player");
            _currentHPText.text = player.GetComponent<Player>().GetCurrentLife().ToString("");
            PlayerStatsManager playerStats = player.GetComponent<PlayerStatsManager>();
            playerStats.AddObserver(this);
        }
        
        private void Start()
        {
            _currentHPText.text = FindObjectOfType<Player>().GetCurrentLife().ToString();
        }
        
        public void UpdateObserver(Player data)
        {
            _currentHPText.text = $"{data.GetCurrentLife().ToString()}";
        }
    }
}