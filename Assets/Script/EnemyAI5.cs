using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float attackDamage = 12f;
    private bool canAttack = true;
    public float attackCooldown = 2f;
    public float invisibilityDuration = 4f;
    public float invisibilityInterval = 10f;

    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking = false;
    private bool isInvisible = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();


        InvokeRepeating("ToggleInvisibility", invisibilityInterval, invisibilityInterval);
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveTowardsPlayer();
            CheckAttackRange();
        }
        if (isInvisible)
        {
            attackDamage =24f;
        }
        else
        {
            attackDamage = 12f;
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    void CheckAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= attackRange && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(Mathf.RoundToInt(attackDamage));
        }

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }
    void ResetAttack()
    {
        canAttack = true;
    }

    void ToggleInvisibility()
    {
        if (isInvisible)
        {
            spriteRenderer.enabled = true;
            isInvisible = false;
        }
        else
        {
            spriteRenderer.enabled = false;
            isInvisible = true;
            Invoke("ToggleInvisibility", invisibilityDuration);
        }
    }
}
