using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Patterns;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public event Action<Vector2> OnEnemyKilled;
    public event Action<ParticleSystem.MinMaxGradient, Vector2> OnExplosion;
    public event Action<float, Vector2> OnDamageDealt;

    public void EnemyKilled(Vector2 position)
    {
        OnEnemyKilled?.Invoke(position);
    }
    public void CreateExplosion(ParticleSystem.MinMaxGradient colors,Vector2 position)
    {
        OnExplosion?.Invoke(colors, position);
    }
    public void DamageDealt(float damage, Vector2 position)
    {
        OnDamageDealt?.Invoke(damage, position);
    }
}
