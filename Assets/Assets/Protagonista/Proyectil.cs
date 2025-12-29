using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private float lifeTimer;
    private ObjectPool objectPool;

    void OnEnable()
    {
        lifeTimer = lifeTime;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            objectPool.ReturnObject(gameObject);
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }
}
