using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPowerZone : MonoBehaviour
{
    public int damage = 10;
    private float cooldown = 1f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            var enemyHealth = other.GetComponent<EnemmiHealth>();
            var bossHealth = other.GetComponent<Boss1Health>();

            if (enemyHealth != null && !enemyHealth.hasBeenHit)
            {
                enemyHealth.TakeDamage(damage);
                enemyHealth.hasBeenHit = true;
                StartCoroutine(ResetHitStatus(enemyHealth));
            }
            else if (bossHealth != null && !bossHealth.hasBeenHit)
            {
                bossHealth.TakeDamage(damage);
                bossHealth.hasBeenHit = true;
                StartCoroutine(ResetHitStatus(bossHealth));
            }
        }
    }
     
    IEnumerator ResetHitStatus(EnemmiHealth enemyHealth)
    {
        yield return new WaitForSeconds(cooldown);
        enemyHealth.hasBeenHit = false; // Réinitialisation après le cooldown
    }

    IEnumerator ResetHitStatus(Boss1Health bossHealth)
    {
        yield return new WaitForSeconds(cooldown);
        bossHealth.hasBeenHit = false; // Réinitialisation après le cooldown
    }
}
