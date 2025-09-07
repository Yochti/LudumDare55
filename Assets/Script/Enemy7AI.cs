using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7AI : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public float damage = 15f;
    private Vector3 targetPosition;
    public float detectionRadius = 8f; 
    private Transform player;
    private Rigidbody2D rb;
    private bool canAttack = true;
    private Transform poutch;
    private EnemyC enemyC;

    void Start()
    {
        enemyC = this.gameObject.GetComponent<EnemyC>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveSpeed = enemyC.currentSpeed;

        if (poutch == null)
        {
            GameObject poutchObject = GameObject.FindGameObjectWithTag("Poutch");
            if (poutchObject != null)
            {
                poutch = poutchObject.transform;
            }
        }
        if (player != null)
        {
            if(poutch != null)
            {
                Vector2 direction = (poutch.position - transform.position).normalized;

                rb.velocity = direction * moveSpeed;
            }
            else if(!PlayerInvisibleZone.isInvisible)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                rb.velocity = direction * moveSpeed;

                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer <= attackRange && canAttack)
                {
                    AttackPlayer();
                }
            }
            else
            {
                MoveTowardsApproximatePosition();
            }
            
        }
    }
    private void MoveTowardsApproximatePosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || targetPosition == Vector3.zero)
        {
            targetPosition = GetRandomPointAround(PlayerInvisibleZone.lastKnownPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);  // Ajuste la vitesse selon tes besoins
    }

    private Vector3 GetRandomPointAround(Vector3 center)
    {
        Vector2 randomPoint = Random.insideUnitCircle * detectionRadius;
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.CompareTag("BulletA"))
        {
            Destroy(collision.gameObject);
        }
    }
    
    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
