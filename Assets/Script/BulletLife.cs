using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamage;
    private bulletBababoyDamage bInt;
    private killAmountStats kStats;
    void Start()
    {
        bInt = FindObjectOfType<bulletBababoyDamage>();
        bulletDamage = 15;
        Destroy(gameObject, bulletLifetime);
        kStats = FindAnyObjectByType<killAmountStats>();

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss1Health>().TakeDamage(bulletDamage);
            bInt.OnHit(bulletDamage);
            if (other.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.IncreaseBababoyKill();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemmiHealth>().TakeDamage(bulletDamage);
            bInt.OnHit(bulletDamage);
            if (other.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.IncreaseBababoyKill();
            }
        }

        if (!other.gameObject.CompareTag("BulletA"))
        {
            Destroy(gameObject);

        }
    }
}
