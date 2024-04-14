using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float bulletLifetime = 8f;
    public int bulletDamage = 20;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemmiHealth>().TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}
