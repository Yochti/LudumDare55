using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeGolemAI : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public int damage = 20;      
    public float attackCooldown = 1.25f;
    public float attackRange = 1.5f;
    private Transform player;    
    private float nextAttackTime = 0f; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    // Se déplace vers le joueur
    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= attackRange) 
            {
                AttackPlayer();
            }
        }
    }

    // Gère l'attaque du joueur
    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
