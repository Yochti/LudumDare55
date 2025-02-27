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
    public float detectionRadius = 8f;   
    private Vector3 targetPosition;      
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private bool isAttacking = false;
    private bool isInvisible = false;
    private Transform poutch;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        InvokeRepeating("ToggleInvisibility", invisibilityInterval, invisibilityInterval);
    }

    void Update()
    {
        if (poutch == null)
        {
            GameObject poutchObject = GameObject.FindGameObjectWithTag("Poutch");
            if (poutchObject != null)
            {
                poutch = poutchObject.transform;
            }
        }
        if(poutch != null)
        {
            MoveTowardsTarget(poutch);
            
        }
        if (!PlayerInvisibleZone.isInvisible)
        {
            if (!isAttacking)
            {
                MoveTowardsTarget(player.transform);
                CheckAttackRange();
            }
        }
        else
        {
            MoveTowardsApproximatePosition();
            CheckAttackRange();
        }
       
        
    }

    void MoveTowardsTarget(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
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
            attackDamage = 12;
            spriteRenderer.enabled = true;
            isInvisible = false;
        }
        else
        {
            attackDamage = 24;
            spriteRenderer.enabled = false;
            isInvisible = true;
            Invoke("ToggleInvisibility", invisibilityDuration);
        }
    }
}
