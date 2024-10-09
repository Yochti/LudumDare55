using UnityEngine;

public class HealingEnemy : MonoBehaviour
{
    public float detectionRadius = 10f; // Rayon de détection des ennemis
    public float healingRange = 2f; // Portée de guérison
    public float healingRate = 10f; // Taux de guérison par seconde
    public float moveSpeed = 3f; // Vitesse de déplacement
    public LayerMask enemyLayerMask; // Masque de collision pour les ennemis
    public LayerMask playerLayerMask; // Masque de collision pour le joueur

    private GameObject targetEnemy; // Ennemi cible à guérir
    private bool isHealing = false; // Indique si l'ennemi est en train de guérir
    private float nextHealTime = 0f; // Temps avant la prochaine guérison

    void Update()
    {
        FindTargetEnemy();

        if (targetEnemy != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, targetEnemy.transform.position);

            if (distanceToTarget <= healingRange)
            {
                if (Time.time >= nextHealTime)
                {
                    HealTarget();
                    nextHealTime = Time.time + 1f / healingRate;
                }
            }
            else
            {
                MoveTowardsTarget(targetEnemy.transform.position);
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void FindTargetEnemy()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayerMask);

        float minHealthPercentage = Mathf.Infinity;

        foreach (Collider2D enemyCollider in enemiesInRange)
        {
            EnemmiHealth enemyHealth = enemyCollider.GetComponent<EnemmiHealth>();
            if (enemyHealth != null)
            {
                float healthPercentage = enemyHealth.currentHealth / enemyHealth.maxHealth;
                if (healthPercentage < minHealthPercentage)
                {
                    minHealthPercentage = healthPercentage;
                    targetEnemy = enemyCollider.gameObject;
                }
            }
        }
    }

    void MoveTowardsTarget(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayerMask);
        if (players.Length > 0)
        {
            // Si le joueur est détecté, se diriger vers lui
            MoveTowardsTarget(players[0].transform.position);
        }

    }

    void HealTarget()
    {
        EnemmiHealth enemyHealth = targetEnemy.GetComponent<EnemmiHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.Heal(5);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, healingRange);
    }
}
