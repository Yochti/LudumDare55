using UnityEngine;

public class BulletTurretLife : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamage = 15;
    private playerBulletDamage bInt;
    private killAmountStats kStats;

    void Start()
    {
        kStats = FindAnyObjectByType<killAmountStats>();
        bInt = FindObjectOfType<playerBulletDamage>();
        bulletDamage = 15;
        Destroy(gameObject, bulletLifetime);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss1Health>().TakeDamage(bulletDamage);
            bInt.onHitTurret1(bulletDamage);
            if (other.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.Increaseturret1kill();
            }
        }   
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemmiHealth>().TakeDamage(bulletDamage);
            bInt.onHitTurret1(bulletDamage);
            if (other.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.Increaseturret1kill();

            }
        }

        if (!other.gameObject.CompareTag("BulletA"))
        {
            Destroy(gameObject);

        }
    }
}
