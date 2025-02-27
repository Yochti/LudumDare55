
using UnityEngine;

public class SoulCollector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; 
    public GameObject spriteRenderer1; 
    public float attractSpeed = 20f;
    public float destructionDelay = 5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AttractSoulsToPlayer(other.transform);
            Invoke("DestroyCollector", destructionDelay);
            spriteRenderer.enabled = false;
            spriteRenderer1.SetActive(false);
        }
    }

    private void AttractSoulsToPlayer(Transform playerTransform)
    {
        GameObject[] souls = GameObject.FindGameObjectsWithTag("Soul");

        foreach (GameObject soul in souls)
        {
            StartCoroutine(MoveSoulTowardsPlayer(soul, playerTransform));
        }
    }

    private System.Collections.IEnumerator MoveSoulTowardsPlayer(GameObject soul, Transform playerTransform)
    {
        while (soul != null && Vector2.Distance(soul.transform.position, playerTransform.position) > 0.1f)
        {
            Vector2 direction = (playerTransform.position - soul.transform.position).normalized;
            soul.transform.position = Vector2.MoveTowards(soul.transform.position, playerTransform.position, attractSpeed * Time.deltaTime);

            yield return null;
        }

        if (soul != null)
        {
            PlayerSoulsCollect.soulValue++; 
            Destroy(soul);
        }
    }
    private void DestroyCollector()
    {
        Destroy(gameObject); 
    }
}

