using UnityEngine;
using System.Collections;

public class FreezeTrap : MonoBehaviour
{
    public float attractionRadius = 5f;  // Rayon de l'attraction
    public float attractionSpeed = 3f;   // Vitesse d'attraction
    public float attractionDuration = 3f; // Durée de l'attraction

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(AttractEnemies());
            Destroy(gameObject, 1.5f);
        }
    }

    private IEnumerator AttractEnemies()
    {
        float elapsedTime = 0f;

        while (elapsedTime < attractionDuration)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attractionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    Transform enemyTransform = collider.transform;
                    enemyTransform.position = Vector2.MoveTowards(
                        enemyTransform.position,
                        transform.position,
                        attractionSpeed * Time.deltaTime
                    );
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame
        }
    }
}
