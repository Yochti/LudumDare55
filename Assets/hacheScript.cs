using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacheScript : MonoBehaviour
{
    public Transform player;              
    public float rotationSpeed = 200f;    
    public float orbitDistance = 2f;       
    public int damage = 20;                
    public float hitCooldown = 0.2f;     
    private float lastHitTime;          

    public int index;                      
    private float startingAngle;           

    private void Start()
    {
        lastHitTime = -hitCooldown;       

        startingAngle = index * (360f / 3f) * Mathf.Deg2Rad;
    }

    void Update()
    {
        float angle = startingAngle + (rotationSpeed * Time.time * Mathf.Deg2Rad); 
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitDistance;

        transform.position = player.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Time.time >= lastHitTime + hitCooldown)
        {
            EnemmiHealth enemyHealth = collision.GetComponent<EnemmiHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                lastHitTime = Time.time;  
            }
        }

        if (collision.CompareTag("Boss") && Time.time >= lastHitTime + hitCooldown)
        {
            Boss1Health bossHealth = collision.GetComponent<Boss1Health>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                lastHitTime = Time.time;  
            }
        }
    }
}
