using UnityEngine;

public class DamageTrape : MonoBehaviour
{
    public float activationRange = 3f;
    public float damageInterval = 1f; 
    public int damageAmount = 10;
    public float trapDuration = 10f;
    public float cooldownTime = 10f; 
    private bool isActive = false;
    private float lastActivationTime;
    private playerBulletDamage tInt;
    private killAmountStats kStats;


    void Start()
    {
        kStats = FindAnyObjectByType<killAmountStats>();
        tInt = FindObjectOfType<playerBulletDamage>();
        lastActivationTime = -cooldownTime;
    }

    void Update()
    {
        if (!isActive && Time.time - lastActivationTime >= cooldownTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, activationRange);
            
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    ActivateTrap();
                    break;
                }
            }
        }
    }

    void ActivateTrap()
    {
        isActive = true;
        lastActivationTime = Time.time;

        InvokeRepeating("DealDamage", 0f, damageInterval);
        Invoke("DestroyTrap", trapDuration + 15f); 
    }

    void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, activationRange);
        int nOfEnemy = colliders.Length;
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
                tInt.OnHitTrap(damageAmount * nOfEnemy);
                kStats.IncreaseTrap3Kill();


            }
        }
    }



    void DestroyTrap()
    {
        Destroy(gameObject); // Self-destruct the trap
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}
