using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionBullet2 : MonoBehaviour
{
    public float damageRadius = 15f; // Rayon des dégâts de l'explosion
    public int damageAmount = 60;
    public int bulletDamagemin;
    public int bulletDamagemax;
    public float lifetime = 8f; // Durée de vie de la balle avant sa destruction
    private bool isAttached = false; // Indique si la balle est attachée à un ennemi
    private Transform attachedTarget; // La cible à laquelle la balle est attachée
    public bool isCritical;
    public int critChance = 15; 
    public float critDamage = 200f;
    private saveSytem save;

    private void Start()
    {
        StartCoroutine(waitingForAwake());

        save = FindObjectOfType<saveSytem>();

        critChance = PlayerStats.critRate;
        critDamage = PlayerStats.critDamage;




        damageAmount = Random.Range(
            Mathf.FloorToInt(bulletDamagemin * (1 + PlayerStats.additionalDamage)),
            Mathf.FloorToInt(bulletDamagemax * (1 + PlayerStats.additionalDamage)) + 1
        );

        if (Random.Range(1, 100) <= critChance)
        {
            damageAmount = (int)(damageAmount * (critDamage / 100f));
            isCritical = true;
        }
        Invoke("ApplyDamage", lifetime);
    }

    private void Update()
    {
        // Si la balle est attachée à un ennemi, elle doit suivre les mouvements de l'ennemi
        if (isAttached && attachedTarget != null)
        {
            transform.position = attachedTarget.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Boss"))
        {
            isAttached = true;
            attachedTarget = collision.transform;

            GetComponent<Collider2D>().enabled = false;

        }
    }

    private void ApplyDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);
        int numColliders = colliders.Length;

        foreach (Collider2D collider in colliders)
        {

            if (collider.CompareTag("Enemy"))
            {
                DamagePopUp.Create(collider.transform.position, damageAmount, isCritical); ;
                collider.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
                
            }
            if (collider.CompareTag("Boss"))
            {
                DamagePopUp.Create(collider.transform.position, damageAmount, isCritical); ;
                collider.GetComponent<Boss1Health>().TakeDamage(damageAmount);
                
            }
        }
        Destroy(gameObject);
    }
    IEnumerator waitingForAwake()
    {
        yield return new WaitForSeconds(0.0001f);
    }
}
