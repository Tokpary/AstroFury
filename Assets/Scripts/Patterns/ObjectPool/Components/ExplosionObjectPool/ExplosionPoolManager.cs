using System.Collections;
using System.Collections.Generic;
using Patterns.ObjectPool.Components;
using UnityEngine;

public class ExplosionPoolManager : AObjectPoolManager<Explosion>
{
    private void OnEnable()
    {
        EventManager.Instance.OnExplosion += CreateExplosion;
    }
        
    private void OnDisable()
    {
        EventManager.Instance.OnExplosion -= CreateExplosion;
    }
        
    private void CreateExplosion(ParticleSystem.MinMaxGradient colors, Vector2 position)
    {
        Explosion explosion = CreatePoolElement(elementPrototype, initialNumberOfElements, allowAddNewElements);
        explosion.transform.position = position;
        explosion.StartExplosion(colors);
    }
}
