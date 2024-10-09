using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal3 : MonoBehaviour
{
    public GameObject bulletExplosionPrefab;
    public float explosionForce = 1000f;
    public int numberOfBullets = 0;
    public bool ThreeBulletsPassed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            numberOfBullets++;
            GameObject explosion = Instantiate(bulletExplosionPrefab, collision.transform.position, Quaternion.identity);
            Rigidbody2D explosionRigidbody = explosion.GetComponent<Rigidbody2D>();

            // Vérifie que l'objet instancié a bien un composant Rigidbody2D
            if (explosionRigidbody != null)
            {
                // Attribue une vitesse au Rigidbody2D de l'objet instancié
                explosionRigidbody.velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity * 2f;
            }

            Destroy(collision.gameObject);
        }
    }
}
