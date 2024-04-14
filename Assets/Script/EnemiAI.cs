using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int damage = 10;

    private Transform player;
    private Rigidbody2D rb;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                AttackPlayer();
            }
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
