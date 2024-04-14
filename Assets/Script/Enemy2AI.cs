using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    private Transform player; // Référence vers le joueur
    public GameObject bulletPrefab; // Préfabriqué de la balle
    public float moveSpeed = 5f; // Vitesse de déplacement de l'ennemi
    public float fireRate = 2f; // Cadence de tir en secondes
    public float bulletSpeed = 10f; // Vitesse de la balle

    private float nextFireTime; // Temps avant le prochain tir

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            Vector2 direction = player.transform.position - transform.position;
            transform.up = direction.normalized;

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; 
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;

        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10); 
            }

            // Détruire l'ennemi après la collision
            Destroy(gameObject);
        }
    }
}
