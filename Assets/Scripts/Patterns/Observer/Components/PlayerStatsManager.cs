using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using Patterns.Observer.Interfaces;
using Patterns.State.Interfaces;
using ScriptableObjects;
using Scripts;
using Scripts.Patterns.State.Components;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatsManager : MonoBehaviour, ISubject<Player>
{
    private Player _player;
    
    private List<Patterns.Observer.Interfaces.IObserver<Player>> _observers = new List<Patterns.Observer.Interfaces.IObserver<Player>>();
    
    public void AddObserver(Patterns.Observer.Interfaces.IObserver<Player> observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(Patterns.Observer.Interfaces.IObserver<Player> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (Patterns.Observer.Interfaces.IObserver<Player> observer in _observers)
        {
            observer?.UpdateObserver(_player);
        }
    }
    
    private void Start()
    {
        _player = GetComponent<Player>();
        GameManager.Instance.OnUpgradeAcquired.AddListener(PlayerAddUpgradeStats);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnUpgradeAcquired.RemoveListener(PlayerAddUpgradeStats);
    }

    private void PlayerAddUpgradeStats(UpgradeItemSO upgradeItem)
    {
        foreach (var upgrade in upgradeItem.StatUpgrades)
        {
            PlayerAddUpgrade(upgrade);
        }
    }

    private void PlayerAddUpgrade(StatUpgradeSO upgrade)
    {
        switch (upgrade.StatType)
        {
            case StatType.MaxHealthUp:                      // AJUSTADO
                UpdatePlayerMaxHealth(upgrade.StatValue);
                break;
            case StatType.HealthUp:                         // AJUSTADO
                UpdatePlayerHealth(upgrade.StatValue);
                break;
            case StatType.Damage:                       // AJUSTADO
                UpdatePlayerDamage(upgrade.StatValue);
                break;
            case StatType.Speed:                        // AJUSTADO
                UpdatePlayerSpeed(upgrade.StatValue);
                break;
           // case StatType.Drift:                        // DESCARTADO
           //     UpdatePlayerDrift(upgrade.StatValue);
           //     break;
            case StatType.Range:                        // AJUSTADO
                UpgradePlayerRange(upgrade.StatValue);
                break;
            case StatType.CritChance:                   // AJUSTADO
                UpgradePlayerCritChance(upgrade.StatValue);
                break;
            case StatType.CritDamage:                   // AJUSTADO
                upgradePlayerCritDamage(upgrade.StatValue);
                break;
            case StatType.ShotSpeed:                    
                upgradePlayerShotSpeed(upgrade.StatValue);
                break;
            default:
                break;
        }
        
        
        NotifyObservers();
    }



    private void UpdatePlayerMaxHealth(float upgradeStatValue)
    {
        _player.MaxLife += (int) upgradeStatValue;
    }
    
    private void UpdatePlayerHealth(float upgradeStatValue)
    {
        _player.TakeDamage((int) -upgradeStatValue); 
    }
    
    private void UpdatePlayerDamage(float upgradeStatValue)
    {
        _player.Damage += upgradeStatValue / 5;
        
        if (_player.Damage < 1f)
            _player.Damage = 1f;
    }
    
    private void UpdatePlayerSpeed(float upgradeStatValue)
    {
        if(_player.Speed < 15f)
            _player.Speed += upgradeStatValue / 10f;
        else
            _player.Speed = 15f;
    }
    
    private void UpgradePlayerRange(float upgradeStatValue) 
    {
        if(_player.Range < 20f)
            _player.Range += upgradeStatValue / 5f; 
        else
            _player.Range = 20f;
        
        if(_player.Range < 1f)
            _player.Range = 1f;
    }
    
    private void UpgradePlayerCritChance(float upgradeStatValue)
    {
        if(_player.CriticalChance < 100f)
            _player.CriticalChance += upgradeStatValue;
        
        if(_player.CriticalChance < 0f)
            _player.CriticalChance = 0f;
    }
    
    private void upgradePlayerCritDamage(float upgradeStatValue)
    {
        _player.CriticalMultiplier += upgradeStatValue / 100f;
        
        if(_player.CriticalMultiplier < 1.1f)
            _player.CriticalMultiplier = 1.1f;
    }
    
    private void upgradePlayerShotSpeed(float upgradeStatValue)
    {
        if(_player.FireRate < 100f)
            _player.FireRate += upgradeStatValue;
        else
        {
            _player.FireRate = 99f;
        }
    }
    
    private void UpdatePlayerDrift(float upgradeStatValue)
    {
        if(_player.Drift < 100f)
            _player.Drift += upgradeStatValue;
        
        float newMass = (100f - _player.Drift)/100f;
        
        if (newMass < 0)
        {
            _player.Drift = 100;
            newMass = 0.05f;
        }
        
        _player.GetComponent<Rigidbody2D>().mass = newMass;
    }

}
