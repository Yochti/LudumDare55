using UnityEngine;

public class EnemyAI3: MonoBehaviour
{
    private Transform  player; 
    public float moveSpeed = 15f; 
    public float explosionRadius = 2f; 
    public float explosionDamage = 20f; 

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
