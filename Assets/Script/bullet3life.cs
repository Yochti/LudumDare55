using UnityEngine;

public class Bullet3life : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamage;
    private playerBulletDamage fois3damage;
    private killAmountStats kStats;
    public bool isCritical;

    void Start()
    {
        kStats = FindAnyObjectByType<killAmountStats>();

        fois3damage = FindObjectOfType<playerBulletDamage>();
        bulletDamage = Random.Range(10, 21);

        if (Random.Range(1, 7) == 1)
        {
            bulletDamage = (int)(bulletDamage * 2.0f);
            isCritical = true;
        }
        Destroy(gameObject, bulletLifetime);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss1Health>().TakeDamage(bulletDamage);
            fois3damage.OnHitFois3();
            DamagePopUp.Create(other.transform.position, bulletDamage, isCritical); ;

            if (other.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.IncreaseFois3Kill();
            }
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemmiHealth>().TakeDamage(bulletDamage);
            fois3damage.OnHitFois3();
            DamagePopUp.Create(other.transform.position, bulletDamage, isCritical); ;

            if (other.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.IncreaseFois3Kill();
            }
        }

        if (!other.gameObject.CompareTag("BulletA"))
        {
            Destroy(gameObject);

        }
    }
}
