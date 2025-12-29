using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummarySlot : MonoBehaviour
{
    private StatUpgradeSO _statUpgrade;
    [SerializeField] private Image statIcon;
    [SerializeField] private TMP_Text statName;
    
    public void LoadStatUpgrade(StatUpgradeSO statSO)
    {
        if (statSO == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            statIcon.sprite = statSO.StatIcon;
            statName.text = (statSO.StatValue > 0 ? "+" : "") + statSO.StatValue + " " + statSO.StatName;
            statName.color = statSO.StatValue > 0 ? Color.green : Color.red;
            statIcon.color = statSO.StatValue > 0 ? Color.green : Color.red;
        }
    }

    
}