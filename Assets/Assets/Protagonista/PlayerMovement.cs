using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    public GameObject projectilePrefab;
    public Transform firePoint;
    public ObjectPool projectilePool;
    public float fireRate = 0.5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera mainCamera;
    private float fireTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Obtener la entrada del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // A, D, Izquierda, Derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W, S, Arriba, Abajo

        // Rotar el personaje para que siga al ratón
        RotateTowardsMouse();

        // Disparar proyectiles
        fireTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer <= 0)
        {
            Shoot();
            fireTimer = 1f / fireRate;
        }
    }

    void FixedUpdate()
    {
        // Mover el personaje
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        newPosition = ClampPositionToScreen(newPosition);
        rb.MovePosition(newPosition);
    }

    void RotateTowardsMouse()
    {
        // Obtener la posición del ratón en coordenadas del mundo
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calcular el ángulo en el que el personaje debe rotar
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Aplicar la rotación
        rb.rotation = angle;
    }

    Vector2 ClampPositionToScreen(Vector2 position)
    {
        // Obtener los límites de la pantalla en coordenadas del mundo
        Vector2 minScreenBounds = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Limitar la posición del personaje a los límites de la pantalla
        position.x = Mathf.Clamp(position.x, minScreenBounds.x, maxScreenBounds.x);
        position.y = Mathf.Clamp(position.y, minScreenBounds.y, maxScreenBounds.y);

        return position;
    }

    void Shoot()
    {
        GameObject projectile = projectilePool.GetObject();
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.GetComponent<Proyectil>().SetObjectPool(projectilePool);
        }
    }
}
