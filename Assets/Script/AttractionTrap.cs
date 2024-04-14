using UnityEngine;
using System.Collections;
public class FreezeTrap : MonoBehaviour
{
    public float attractionRadius = 5f; // Rayon d'attraction
    public float attractionForce = 10000f; // Force d'attraction
    public float duration = 3f; // Durée de l'attraction en secondes
    public LayerMask enemyLayer; // Masque de couche pour les ennemis

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            AttractEnemies();
            Destroy(gameObject);

        }
    }

    private void AttractEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attractionRadius, enemyLayer);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; 
                StartCoroutine(DisableRigidbody(rb)); 
            }
        }
    }

    private IEnumerator DisableRigidbody(Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll; 
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
