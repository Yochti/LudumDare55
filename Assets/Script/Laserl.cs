using UnityEngine;

public class Laserle : MonoBehaviour
{
    public int damagePerSecond = 20; 
    private float damageInterval = 0.01f; 
    private float damageTimer = 0f;
    private playerBulletDamage LInt;
    private killAmountStats kStats;

    private void Start()
    {
        kStats = FindAnyObjectByType<killAmountStats>();
        LInt = FindObjectOfType<playerBulletDamage>();
    }
    void Update()
    {
        if (damageTimer >= damageInterval)
        {
            damageTimer = 0f; 
        }
        else
        {
            damageTimer += Time.deltaTime; 
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (damageTimer <= 0f) 
        {
            if (other.CompareTag("Enemy")) 
            {
                DealDamage(other.gameObject); 
            }
            if (other.CompareTag("Boss")) 
            {
                DealBossDamage(other.gameObject); 
            }
        }
    }

    void DealDamage(GameObject target)
    {
        EnemmiHealth enemyHealth = target.GetComponent<EnemmiHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damagePerSecond);
            LInt.onHitTurret3(damagePerSecond);
            kStats.IncreaseTuret3Kill();

        }
    }
    void DealBossDamage(GameObject target)
    {
        Boss1Health bHP = target.GetComponent<Boss1Health>();
        if(bHP != null)
        {
            bHP.TakeDamage(damagePerSecond);
            LInt.onHitTurret3(damagePerSecond);
            kStats.IncreaseTuret3Kill();
        }
    }
}
