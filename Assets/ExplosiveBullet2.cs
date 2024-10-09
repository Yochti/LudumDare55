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
        save = FindObjectOfType<saveSytem>();

        if (save.PlayerCritchance != 0)
            critChance = save.PlayerCritchance;
        else
            critChance = 15;
        if (save.PlayerCritDamages != 0)
            critDamage = save.PlayerCritDamages;




        damageAmount = Random.Range(bulletDamagemin + save.PlayerDamages, bulletDamagemax + save.PlayerDamages);

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
                collider.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
                
            }
            if (collider.CompareTag("Boss"))
            {
                collider.GetComponent<Boss1Health>().TakeDamage(damageAmount);
                
            }
        }
        Destroy(gameObject);
    }
}
