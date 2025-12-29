using Patterns.Observer.Interfaces;
using TMPro;
using UnityEngine;

namespace Patterns.Observer.Components
{
    public class ObserverCurrencyUI : MonoBehaviour, IObserver<float>
    {
        private TMP_Text _currencyText;
        private void Start()
        {
            _currencyText = GetComponent<TMP_Text>();
            _currencyText.text = "0";
            GameObject player = GameObject.FindWithTag("Player");
            CurrencyManager currency = player.GetComponentInChildren<CurrencyManager>();
            currency.AddObserver(this);
        }
        
        public void UpdateObserver(float data)
        {
            _currencyText.text = $"{data}";
        }
    }
}