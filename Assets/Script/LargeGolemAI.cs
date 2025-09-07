using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeGolemAI : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public int damage = 20;      
    public float attackCooldown = 1.25f;
    public float attackRange = 1.5f;
    private Transform player;    
    private float nextAttackTime = 0f;
    public float detectionRadius = 8f;
    private Vector3 targetPosition;
    private EnemyC enemyC;
    private void Start()
    {
        enemyC = this.gameObject.GetComponent<EnemyC>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        moveSpeed = enemyC.currentSpeed;
        if(!PlayerInvisibleZone.isInvisible)
            MoveTowardsPlayer();
        else
        {
            MoveTowardsApproximatePosition();
            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
    }

    // Se déplace vers le joueur
    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= attackRange) 
            {
                AttackPlayer();
            }
        }
    }
    private void MoveTowardsApproximatePosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || targetPosition == Vector3.zero)
        {
            targetPosition = GetRandomPointAround(PlayerInvisibleZone.lastKnownPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    private Vector3 GetRandomPointAround(Vector3 center)
    {
        Vector2 randomPoint = Random.insideUnitCircle * detectionRadius;
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, transform.position.z);
    }

    // Gère l'attaque du joueur
    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
