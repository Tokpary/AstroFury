using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Patterns.Observer.Components;
using Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceContainerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _containerImg;
    private CurrencyManager _currency;
    
    public void NotEnoughCurrencyError()
    {
        transform.DOShakePosition(0.5f, 10, 90, 90, false, true);
    }
    
    public void SetItemSlotPrice(int price)
    {
        _currency = GameObject.FindWithTag("Player").GetComponentInChildren<CurrencyManager>();
        if(price > _currency.Currency)
        {
            _containerImg.color = Color.red;
        }
        else
        {
            _containerImg.color = Color.cyan;
        }
        _priceText.text = price.ToString();
    }

}
