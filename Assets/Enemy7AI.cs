using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7AI : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float attackRange = 3f;
    public float attackCooldown = 2f;
    public float damage = 15f;

    private Transform player;
    private Rigidbody2D rb;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                AttackPlayer();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.CompareTag("BulletA"))
        {
            Destroy(collision.gameObject);
        }
    }
    
    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);

        canAttack = false;
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
