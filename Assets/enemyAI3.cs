using UnityEngine;

public class EnemyAI3: MonoBehaviour
{
    private Transform  player; // Référence vers le joueur
    public float moveSpeed = 15f; // Vitesse de déplacement de l'ennemi
    public float explosionRadius = 2f; // Rayon de l'explosion
    public int explosionDamage = 50; // Dégâts de l'explosion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) <= explosionRadius)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(explosionDamage);
                }
            }
        }

        Destroy(gameObject);
    }
}
