using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonBoss : MonoBehaviour
{
    public float damage = 5f;
    public float lifetime = 30f;
    private void Start()
    {
        Invoke("DtroyGO", lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (collision.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    void DtroyGO()
    {
        Destroy(gameObject);
    }
}
