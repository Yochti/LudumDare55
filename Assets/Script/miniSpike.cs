using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpike : MonoBehaviour
{
    public float speed = 7f; // Vitesse des projectiles divisés
    public int damage = 10; // Dégâts infligés par chaque projectile divisé
    public float lifespan = 2f; // Durée de vie des projectiles
    public int bulletDamagemin;
    public int bulletDamagemax;
    private Vector3 startPoint;
    private saveSytem save;
    public bool isCritical;
    public int critChance = 15;
    public float critDamage = 200f;
    private playerBulletDamage pDamage;
    private killAmountStats kStats;
    void Start()
    {
        save = FindObjectOfType<saveSytem>();

        critChance = PlayerStats.critRate;
        critDamage = PlayerStats.critDamage;
        startPoint = transform.position;
        pDamage = FindAnyObjectByType<playerBulletDamage>();
        kStats = FindAnyObjectByType<killAmountStats>();
        damage = Random.Range(
            Mathf.FloorToInt(bulletDamagemin * (1 + PlayerStats.additionalDamage)),
            Mathf.FloorToInt(bulletDamagemax * (1 + PlayerStats.additionalDamage)) + 1
        );

        if (Random.Range(1, 100) <= critChance)
        {
            damage = (int)(damage * (critDamage / 100f));
            isCritical = true;
        }
        
        
        
        Destroy(gameObject, lifespan); // Détruit le projectile après un certain temps
    }

    void Update()
    {
        // Déplacement du projectile
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemmiHealth>().TakeDamage(damage);
            DamagePopUp.Create(other.transform.position, damage, isCritical);
            pDamage.OnHit(damage);
            if (other.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }
            Destroy(this.gameObject);

        }
        if (other.CompareTag("Boss"))
        {
            other.GetComponent<Boss1Health>().TakeDamage(damage);
            DamagePopUp.Create(other.transform.position, damage, isCritical);
            pDamage.OnHit(damage);
            if (other.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }
            Destroy(this.gameObject);
        }
    }
}
