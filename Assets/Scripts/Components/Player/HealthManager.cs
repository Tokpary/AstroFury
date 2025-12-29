using System.Collections;
using System.Collections.Generic;
using Patterns.Observer.Interfaces;
using Scripts;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour, IObserver<Player>
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _maxHealthSprite;
    [SerializeField] private Sprite _halfHealthSprite;
    [SerializeField] private Sprite _lowHealthSprite;
    [SerializeField] private Sprite _criticalHealthSprite;
    
    private void Awake()
    {
        _spriteRenderer = GetComponentInParent<SpriteRenderer>();
        PlayerStatsManager playerStats = GetComponentInParent<PlayerStatsManager>();
        playerStats.AddObserver(this);
    }
        
    private void Start()
    {
        _spriteRenderer.sprite = _maxHealthSprite;
    }
        
    public void UpdateObserver(Player data)
    {
        if (data.GetCurrentLife() >= data.MaxLife * 0.75)
        {
            _spriteRenderer.sprite = _maxHealthSprite;
        }
        else if (data.GetCurrentLife() >= data.MaxLife * 0.5)
        {
            _spriteRenderer.sprite = _halfHealthSprite;
        }
        else if (data.GetCurrentLife() >= data.MaxLife * 0.25)
        {
            _spriteRenderer.sprite = _lowHealthSprite;
        }
        else
        {
            _spriteRenderer.sprite = _criticalHealthSprite;
        }
    }
}
