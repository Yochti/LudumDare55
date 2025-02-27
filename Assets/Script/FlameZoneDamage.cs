using System.Collections;
using UnityEngine;

public class FlameZoneDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float timeLife = 5f;
    public float damageInterval = 0.8f; 

    private bool canDealDamage = true; 

    private void Start()
    {
        lifetime();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDealDamage) 
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemmiHealth enemyHealth = collision.GetComponent<EnemmiHealth>();
                if (enemyHealth != null) 
                {
                    enemyHealth.TakeDamage(damageAmount);
                    StartCoroutine(WaitForDamages());
                }
            }
            else if (collision.CompareTag("Boss"))
            {
                Boss1Health bossHealth = collision.GetComponent<Boss1Health>();
                if (bossHealth != null) 
                {
                    bossHealth.TakeDamage(damageAmount);
                    StartCoroutine(WaitForDamages());
                }
            }
        }
    }

    void lifetime()
    {
        Destroy(this.gameObject, timeLife);
    }

    IEnumerator WaitForDamages()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(damageInterval);
        canDealDamage = true;
    }
}
