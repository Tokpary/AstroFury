using System;
using System.Collections;
using System.Collections.Generic;
using Patterns.ObjectPool.Interfaces;
using UnityEngine;

public class Explosion : MonoBehaviour, IPooleableObject
{
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public IPooleableObject Clone()
    {
        GameObject clone = Instantiate(gameObject);
        Explosion explosion = clone.GetComponent<Explosion>();
        return explosion;
    }

    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }
    public IObjectPool elementPool { get; set; }
    public void Reset()
    {
        _particleSystem.Clear();
    }

    public void StartExplosion(ParticleSystem.MinMaxGradient colors)
    {
        SetParticleSystemColor(colors);
        _particleSystem.Play();
        StartCoroutine(AwaitAnimationEndAndRelease());
    }

    private void SetParticleSystemColor(ParticleSystem.MinMaxGradient colors)
    {
        var main = _particleSystem.main;
        main.startColor = new ParticleSystem.MinMaxGradient(colors.colorMin, colors.colorMax);
    }
    
    IEnumerator AwaitAnimationEndAndRelease()
    {
        yield return new WaitForSeconds(_particleSystem.main.duration);
        elementPool?.Release(this);
    }
}
