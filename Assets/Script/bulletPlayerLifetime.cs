using UnityEngine;
using System.Collections;
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
    public int critChance;
    private saveSytem save;
    public float critDamage = 200f;
    void Start()
    {
        StartCoroutine(waitingForAwake());

        playerAk = FindObjectOfType<PlayerShootingAk>();
        save = FindObjectOfType<saveSytem>();
        critChance = PlayerStats.critRate;
        critDamage = PlayerStats.critDamage;
        
        
        pDamage = FindAnyObjectByType<playerBulletDamage>();
        kStats = FindAnyObjectByType<killAmountStats>();

        bulletDamage = Random.Range(
            Mathf.FloorToInt(bulletDamagemin * (1 + PlayerStats.additionalDamage)),
            Mathf.FloorToInt(bulletDamagemax * (1 + PlayerStats.additionalDamage)) + 1
        );
        print("crit Chance" + critChance + "Crit damage" + critDamage);
        if (Random.Range(1, 100) <= critChance)
        {
            bulletDamage = (int)(bulletDamage * (critDamage /100f));
            isCritical = true;
        }
        print(bulletDamage);
        Destroy(gameObject, bulletLifetime);
        print(PlayerStats.additionalDamage);
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
            if (playerAk != null)
                playerAk.timeSinceLastShot += .045f;


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
            if (playerAk != null)
                playerAk.timeSinceLastShot += .045f;


        }
        if (!other.gameObject.CompareTag("BulletA") || !other.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }


    }
    IEnumerator waitingForAwake()
    {
        yield return new WaitForSeconds(0.0001f);
    }
}
