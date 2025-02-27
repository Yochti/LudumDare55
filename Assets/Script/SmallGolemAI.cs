using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGolemAI : MonoBehaviour
{
    public float detectionRadius = 10f;   // Rayon de d�tection pour fusionner avec d'autres petits golems
    public float moveSpeed = 2f;          // Vitesse de d�placement
    public GameObject largeGolemPrefab;   // Pr�fabriqu� du grand golem
    public float playerDetectionRadius = 15f; // Rayon de d�tection du joueur
    public int damage = 10;               // D�g�ts inflig�s au joueur
    public float attackCooldown = 1f;     // Temps entre chaque attaque du joueur

    private bool isMerging = false;       // Est-ce que ce golem est en train de fusionner ?
    private Transform player;             // R�f�rence au joueur
    private float nextAttackTime = 0f;    // Temps de la prochaine attaque autoris�e

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isMerging)
        {
            MoveTowardsOtherSmallGolem();
        }
    }

    private void MoveTowardsOtherSmallGolem()
    {
        Collider2D[] nearbyGolems = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        GameObject closestGolem = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D golemCollider in nearbyGolems)
        {
            if (golemCollider != null && golemCollider.gameObject != this.gameObject && !golemCollider.GetComponent<SmallGolemAI>().isMerging)
            {
                float distance = Vector2.Distance(transform.position, golemCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGolem = golemCollider.gameObject;
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }

        if (closestGolem != null)
        {
            Vector2 direction = (closestGolem.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, closestGolem.transform.position, moveSpeed * Time.deltaTime);

            if (closestDistance <= 0.5f) // Si le golem est tr�s proche, on fusionne
            {
                StartCoroutine(FuseWithGolem(closestGolem));
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= 0.5f)
            {
                AttackPlayer();
            }
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

    private IEnumerator FuseWithGolem(GameObject otherGolem)
    {
        isMerging = true;
        otherGolem.GetComponent<SmallGolemAI>().isMerging = true;

        yield return new WaitForSeconds(0.5f);

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
