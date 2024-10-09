using UnityEngine;

public class BulletLifEnemy : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public float bulletDamage = 8f;

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
