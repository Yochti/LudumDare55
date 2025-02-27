using UnityEngine;
using System.Collections;

public class MineTrap : MonoBehaviour
{
    public float explosionRadius = 3f;
    public int explosionDamage = 50;
    public AudioSource explosionSFX;
    private playerBulletDamage mInt;
    private killAmountStats kStats;

    private void Start()
    {
        kStats = FindObjectOfType<killAmountStats>();
        mInt = FindObjectOfType<playerBulletDamage>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionSFX != null)
        {
            explosionSFX.Play();
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        int numberOfEnemy = colliders.Length;

        foreach (Collider2D collider in colliders)
        {
            EnemmiHealth enemyHealth = collider.GetComponent<EnemmiHealth>();
            Boss1Health bossHealth = collider.GetComponent<Boss1Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
                mInt.OnHitMine(explosionDamage * numberOfEnemy);

                if (enemyHealth.currentHealth <= 0)
                {
                    kStats.IncreaseMineKill();
                }
            }
            else if (bossHealth != null)
            {
                bossHealth.TakeDamage(explosionDamage);
                mInt.OnHitMine(explosionDamage * numberOfEnemy);

                if (bossHealth.currentHealth <= 0) 
                {
                    kStats.IncreaseMineKill();
                }
            }
        }

        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.ShakeCamera(4f, 0.2f);
        }

        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
