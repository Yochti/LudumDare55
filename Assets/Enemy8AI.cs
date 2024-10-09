using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8AI : MonoBehaviour
{
    public float detectionRadius = 10f;   // Rayon de détection pour fusionner avec d'autres petits golems
    public float moveSpeed = 5f;          // Vitesse de déplacement
    public GameObject largeGolemPrefab;   // Préfabriqué du grand golem
    public float playerDetectionRadius = 15f; // Rayon de détection du joueur
    public int damage = 10;               // Dégâts infligés au joueur
    public float attackCooldown = 1f;     // Temps entre chaque attaque du joueur
    public float attackRange = 2.5f;
    private bool isMerging = false;       // Est-ce que ce golem est en train de fusionner ?
    private Transform player;             // Référence au joueur
    private float nextAttackTime = 0f;    // Temps de la prochaine attaque autorisée
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (!isMerging)
        {
            GameObject closestGolem = FindClosestGolem();

            if (closestGolem != null)
            {
                MoveTowards(closestGolem.transform.position);

                if (Vector2.Distance(transform.position, closestGolem.transform.position) <= 1.5f)
                {
                    FuseWithGolem(closestGolem);
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    private GameObject FindClosestGolem()
    {
        Collider2D[] nearbyGolems = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        GameObject closestGolem = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D golemCollider in nearbyGolems)
        {
            if (golemCollider != null && golemCollider.gameObject != this.gameObject)
            {
                Enemy8AI otherGolemAI = golemCollider.GetComponent<Enemy8AI>();
                if (otherGolemAI != null && !otherGolemAI.isMerging)
                {
                    float distance = Vector2.Distance(transform.position, golemCollider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestGolem = golemCollider.gameObject;
                    }
                }
            }
        }

        return closestGolem;
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
            
        }
    }


    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void FuseWithGolem(GameObject otherGolem)
    {
        isMerging = true;
        Vector2 fusionPosition = (transform.position + otherGolem.transform.position) / 2;
        Instantiate(largeGolemPrefab, fusionPosition, Quaternion.identity);

        Destroy(otherGolem);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
