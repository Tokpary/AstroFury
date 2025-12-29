using System.Collections;
using System.Collections.Generic;
using Patterns.Observer.Interfaces;
using Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ObserverPlayerSpriteUI : MonoBehaviour, IObserver<Player>
{
    private Image _spriteRenderer;
    private Transform _player;
    
    void Start()
    {
        _spriteRenderer = GetComponent<Image>();
        _player = GameObject.FindWithTag("Player").transform;
        
        PlayerStatsManager playerStats = _player.GetComponent<PlayerStatsManager>();
        playerStats.AddObserver(this);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        _spriteRenderer.transform.rotation = _player.rotation;
    }

    public void UpdateObserver(Player data)
    {
        _spriteRenderer.sprite = data.transform.GetComponent<SpriteRenderer>().sprite;
    }
}
