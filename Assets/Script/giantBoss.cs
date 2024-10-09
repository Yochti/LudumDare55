using UnityEngine;

public class GiantBoss : MonoBehaviour
{
    public EnemmiHealth enemyH;
    public float movementSpeed = 3f;
    public float meleeDamage = 20f;
    public GameObject projectilePrefab;
    public GameObject specialProjectilePrefab;
    public Transform player;

    private bool isSecondPhase = false;
    private float secondPhaseHealthThreshold = 1000f;
    private float attackCooldown = 10f;
    private float specialAttackCooldown = 10f;
    private float attackTimer = 0f;
    private float specialAttackTimer = 0f;



    void Update()
    {
        if (!isSecondPhase && (float)enemyH.currentHealth<= secondPhaseHealthThreshold)
        {
            isSecondPhase = true;
        }

        if (attackTimer <= 0f)
        {
            PerformAttack();
            attackTimer = attackCooldown;
        }

        if (specialAttackTimer <= 0f)
        {
            PerformSpecialAttack();
            specialAttackTimer = specialAttackCooldown;
        }

        MoveTowardsPlayer();

        attackTimer -= Time.deltaTime;
        specialAttackTimer -= Time.deltaTime;
    }

    void PerformAttack()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * 5f;
        Destroy(projectile, 3f);
    }

    void PerformSpecialAttack()
    {
        if (isSecondPhase)
        {
            for (int i = 0; i < 9; i++)
            {
                Vector2 direction = Quaternion.Euler(0, 0, i * 40) * Vector2.up;
                GameObject specialProjectile = Instantiate(specialProjectilePrefab, transform.position, Quaternion.identity);
                specialProjectile.GetComponent<Rigidbody2D>().velocity = direction * 5f;
                Destroy(specialProjectile, 3f);
            }
        }
        else
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayer * 5f;
            Destroy(projectile, 3f);
        }
    }


    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > 2f)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * movementSpeed * Time.deltaTime;
        }
        else
        {
            player.GetComponent<PlayerHealth>().TakeDamage((int)meleeDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyH.currentHealth -= damage;
        if (enemyH.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Actions lorsque le boss est vaincu
        Destroy(gameObject);
    }
}
