using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illusion : MonoBehaviour
{
    public float moveSpeed = 3f;
    private MirageEnemy parentEnemy;
    private Transform player;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetParentEnemy(MirageEnemy enemy)
    {
        parentEnemy = enemy;
    }
    private void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            rb.velocity = direction * moveSpeed;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        }
    }
    void OnDestroy()
    {
        if (parentEnemy != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(parentEnemy.illusionDamage);
            }
        }
    }
}
