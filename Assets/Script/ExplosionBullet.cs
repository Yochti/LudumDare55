using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    public float damageRadius = 15f;
    public int damageAmount = 60;
    public float lifetime = 8f;
    private playerBulletDamage eDamage;
    private killAmountStats kStats;

    private void Start()
    {
        eDamage = FindObjectOfType<playerBulletDamage>();
        kStats = FindAnyObjectByType<killAmountStats>();

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Boss"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
            int numColliders = colliders.Length;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
                    if (collider.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
                    {
                        kStats.IncreaseExplosive();
                    }
                }
                if (collider.CompareTag("Boss"))
                {
                    collider.GetComponent<Boss1Health>().TakeDamage(damageAmount);
                    if (collider.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
                    {
                        kStats.IncreaseExplosive();
                    }
                }
            }

            eDamage.onHitExplosion(numColliders);
            Destroy(gameObject);
        }
    }
}
