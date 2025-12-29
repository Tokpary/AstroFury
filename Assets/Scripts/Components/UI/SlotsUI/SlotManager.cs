using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    private Slot _currentSlot = null;
    public UnityEvent<Slot> onSlotClicked;
    private Slot[] _slots;
    
    private void Awake()
    {
        _slots = GetComponentsInChildren<Slot>();
    }

    private void Start()
    {
        onSlotClicked.AddListener(OnNewSlotSelected);
    }

    public void DisplayNotEnoughCurrencyError()
    {
        _currentSlot.DisplayNotEnoughCurrencyError();
    }

    private void OnNewSlotSelected(Slot slot)
    {
        if(_currentSlot != null)
            _currentSlot.Deselect();
        _currentSlot = slot;
        _currentSlot.Select();
    }
    
    public void LoadSlots(List<UpgradeItemSO> upgradeItems)
    {

        for (int i = 0; i < upgradeItems.Count; i++)
        {
            _slots[i].SetUpSlotManager(this);
            _slots[i].LoadUpgradeItem(upgradeItems[i]);
        }
    }

    public void ClearSlots()
    {
        foreach (var slot in _slots)
        {
            slot.Deselect();
        }
        _currentSlot = null;
    }
    
    public UpgradeItemSO GetCurrentUpgradeItem()
    {
        if(_currentSlot != null)
            return _currentSlot.upgradeItemSO;
        return null;
    }
    
    public void DisplayNotSelectedError()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SlotNotSelectedError();
        }
    }
}
