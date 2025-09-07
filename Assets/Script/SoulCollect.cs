using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoulCollector : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject extraSpriteToDisable;
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
            Invoke(nameof(DestroyCollector), destructionDelay);
            spriteRenderer.enabled = false;
            if (extraSpriteToDisable != null)
                extraSpriteToDisable.SetActive(false);
        }
    }

    private void AttractSoulsToPlayer(Transform playerTransform)
    {
        List<GameObject> souls = new List<GameObject>();
        souls.AddRange(GameObject.FindGameObjectsWithTag("Soul"));
        souls.AddRange(GameObject.FindGameObjectsWithTag("Soul2"));
        souls.AddRange(GameObject.FindGameObjectsWithTag("Soul3"));

        foreach (GameObject soul in souls)
        {
            StartCoroutine(MoveSoulTowardsPlayer(soul, playerTransform));
        }
    }

    private IEnumerator MoveSoulTowardsPlayer(GameObject soul, Transform playerTransform)
    {
        while (soul != null && Vector2.Distance(soul.transform.position, playerTransform.position) > 0.1f)
        {
            Vector2 direction = (playerTransform.position - soul.transform.position).normalized;
            soul.transform.position = Vector2.MoveTowards(soul.transform.position, playerTransform.position, attractSpeed * Time.deltaTime);
            yield return null;
        }

        if (soul != null)
        {
            PlayerSoulsCollect.soulValue++; // Assure-toi que cette variable est bien accessible statiquement.
            Destroy(soul);
        }
    }

    private void DestroyCollector()
    {
        Destroy(gameObject);
    }
}
