using UnityEngine;

public class BulletLifEnemy : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamage = 20;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
