using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public int health;
    public int damageToPlayer = 5;
    public int puntos = 10;

    private ScoreDisplay scoreDisplay;
    
    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
    }


    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        Vector3 move = direction * speed * Time.deltaTime;
        transform.position += move;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("misil"))
        {
            TakeDamage(1);
            collision.gameObject.SetActive(false); // Desactiva el misil
        }

        if (collision.CompareTag("Player"))
        {
            // Obtener el componente de jugador y aplicar daño
           // Movimiento player = collision.GetComponent<Movimiento>();
            if (player != null)
            {
            //    player.TakeDamage(damageToPlayer);
            }

            // Destruir al enemigo
            Destroy(gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if(scoreDisplay != null)
            {
                scoreDisplay.AddScore(puntos);
            }
            Destroy(gameObject); // Destruye al enemigo
        }
    }
}
