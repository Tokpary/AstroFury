using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using Systems.SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UpgradeItemSO upgradeItemSO = null;
    private SlotManager _slotManager;
    [SerializeField] private Image _upgradeIcon;
    [SerializeField] private TMP_Text _upgradeName;
    [SerializeField] private TMP_Text _upgradeDesc;
    [SerializeField] private SummarySlot[] _summarySlots;
    [SerializeField] private PriceContainerManager _priceContainerManager;
    
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
    }

    public void LoadUpgradeItem(UpgradeItemSO upgradeItem)
    {
        upgradeItemSO = upgradeItem;
        _upgradeIcon.sprite = upgradeItem.ItemSprite;
        _upgradeName.text = upgradeItem.ItemName;
        _upgradeDesc.text = upgradeItem.ItemDesc;

        for (int i = 0; i < 3; i++)
        {
            if (i >= upgradeItem.StatUpgrades.Length)
            {
                _summarySlots[i].gameObject.SetActive(false);
            }
            else
            {
                _summarySlots[i].gameObject.SetActive(true);
                _summarySlots[i].LoadStatUpgrade(upgradeItem.StatUpgrades[i]);
            }
        }

        if (upgradeItem.Price == 0)
        {
            _priceContainerManager.gameObject.SetActive(false);
        }
        else{
            _priceContainerManager.SetItemSlotPrice(upgradeItem.Price);
            _priceContainerManager.gameObject.SetActive(true);
        }
        
    }

    public void DisplayNotEnoughCurrencyError()
    {
        _priceContainerManager.NotEnoughCurrencyError();
    }
    
    public void SetUpSlotManager(SlotManager slotManager)
    {
        _slotManager = slotManager;
    }
    
    public void SlotNotSelectedError()
    {
        transform.DOShakePosition(0.5f, 10, 10);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySfx("UIHoverSFX");
        transform.DOScale(1.05f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try
        {
            if(_slotManager.GetCurrentUpgradeItem() != upgradeItemSO)
                transform.DOScale(1f, 0.5f);
        }
        catch
        {
            transform.DOScale(1f, 0.5f);
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot clicked");
        _slotManager.onSlotClicked?.Invoke(this);
    }

    public void Select()
    {
        _outline.enabled = true;
        SoundManager.Instance.PlaySfx("UISelectSFX");
        transform.DOScale(1.05f, 0.5f);
    }
    
    public void Deselect()
    {
        _outline.enabled = false;
        transform.DOScale(1f, 0.5f);
    }
}