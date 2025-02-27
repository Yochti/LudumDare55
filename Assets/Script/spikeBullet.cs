using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeBullet : MonoBehaviour
{
    public float maxDistance = 5f; // Distance maximale avant la division
    public int damage = 0; // Dégâts infligés par la balle principale
    public GameObject miniSpikePrefab; // Préfabriqué des projectiles divisés
    public int numMiniSpikes = 4; // Nombre de projectiles divisés
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
        StartCoroutine(waitingForAwake());

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
    }

    void Update()
    {

        if (Vector3.Distance(startPoint, transform.position) >= maxDistance)
        {
            Split();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la balle touche un ennemi, elle inflige des dégâts et se divise
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemmiHealth>().TakeDamage(damage);
            DamagePopUp.Create(collision.transform.position, damage, isCritical);
            pDamage.OnHit(damage);
            if (collision.gameObject.GetComponent<EnemmiHealth>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }

            Split();
        }if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss1Health>().TakeDamage(damage);
            DamagePopUp.Create(collision.transform.position, damage, isCritical);
            pDamage.OnHit(damage);
            Split();
            if (collision.gameObject.GetComponent<Boss1Health>().currentHealth <= 0)
            {
                kStats.IncreaseKillPlayer();
            }

        }
    }

    void Split()
    {
        float offset = 0.9f;

        for (int i = 0; i < numMiniSpikes; i++)
        {
            float angle = i * (360f / numMiniSpikes);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Vector3 spawnPosition = transform.position + (rotation * Vector3.up) * offset;

            Instantiate(miniSpikePrefab, spawnPosition, rotation);
        }

        Destroy(gameObject);
    }
    IEnumerator waitingForAwake()
    {
        yield return new WaitForSeconds(0.0001f);
    }

}
