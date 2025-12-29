using System.Collections;
using System.Collections.Generic;
using Patterns.Observer.Interfaces;
using ScriptableObjects;
using Scripts;
using UnityEngine;

public class StatValueDisplay : MonoBehaviour, IObserver<Player>
{
    public StatType stat;
    
    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerStatsManager playerStats = player.GetComponent<PlayerStatsManager>();
        playerStats.AddObserver(this);
        playerStats.NotifyObservers();
    }

    public void UpdateObserver(Player data)
    {
        switch (stat)
        {
            case StatType.Damage:
                GetComponent<TMPro.TMP_Text>().text = $"{data.Damage.ToString("F")}";
                break;
            case StatType.ShotSpeed:
                GetComponent<TMPro.TMP_Text>().text = $"{(data.FireRate / 100):P}";
                break;
            case StatType.Speed:
                GetComponent<TMPro.TMP_Text>().text = $"{data.Speed.ToString("F")}";
                break;
            case StatType.Range:
                GetComponent<TMPro.TMP_Text>().text = $"{data.Range.ToString("F")}";
                break;
            case StatType.CritChance:
                GetComponent<TMPro.TMP_Text>().text = $"{(data.CriticalChance / 100):P}";
                break;
            case StatType.CritDamage:
                GetComponent<TMPro.TMP_Text>().text = $"{data.CriticalMultiplier:P}";
                break;
        }
    }
}
