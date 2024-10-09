using UnityEngine;

public class BulletPlayerLifetime : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamagemin;
    public int bulletDamagemax;
    public int bulletDamage;
    private playerBulletDamage pDamage;
    private PlayerShootingAk playerAk;
    private killAmountStats kStats;
    public bool isCritical;
    public int critChance = 15;
    private saveSytem save;
    public float critDamage = 200f;
    void Start()
    {
        playerAk = FindObjectOfType<PlayerShootingAk>();
        save = FindObjectOfType<saveSytem>();

        if (save.PlayerCritchance != 0)
            critChance = save.PlayerCritchance;
        else
            critChance = 15;
        if (save.PlayerCritDamages != 0)
            critDamage = save.PlayerCritDamages;
        
                
        
        pDamage = FindAnyObjectByType<playerBulletDamage>();
        kStats = FindAnyObjectByType<killAmountStats>();

        bulletDamage = Random.Range(bulletDamagemin + save.PlayerDamages, bulletDamagemax + save.PlayerDamages);

        if (Random.Range(1, 100) <= critChance)
        {
            bulletDamage = (int)(bulletDamage * (critDamage /100f));
            isCritical = true;
        }
        print(bulletDamage);
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss1Health>().TakeDamage(bulletDamage);
            pDamage.OnHit(bulletDamage);
            DamagePopUp.Create(other.transform.position, bulletDamage, isCritical); ;

            if (other.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }
            if (playerAk == null)
                return;
            else
                playerAk.timeSinceLastShot += .069f;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemmiHealth>().TakeDamage(bulletDamage);
            pDamage.OnHit(bulletDamage);
            DamagePopUp.Create(other.transform.position, bulletDamage, isCritical); ;

            if (other.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }
            if (playerAk == null)
                return;
            else
                playerAk.timeSinceLastShot += .069f;

        }

        if (!other.gameObject.CompareTag("BulletA") || !other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
