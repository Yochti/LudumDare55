using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocationMovement : MonoBehaviour
{
    public Transform player;
    public float followRadius = 5f;
    public float speed = 3f;
    public float repositionTime = 1.5f;
    public GameObject[] gameObjects;
    public float switchInterval = 15f;
    public int levelThresholdForThirdObject = 3;
    public AlliesSummon invoc;
    private Vector2 targetPosition;
    private int currentObjectIndex = 0;
    private float countdown;

    private void Start()
    {
        if (gameObjects.Length == 0)
        {
            Debug.LogError("Aucun GameObject assigné !");
            return;
        }

        StartCoroutine(RepositionPeriodically());
        StartCoroutine(SwitchGameObjects());
    }

    private void Update()
    {
        MoveTowardsTarget();

        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    }

    IEnumerator RepositionPeriodically()
    {
        while (true)
        {
            ChooseNewPosition();
            yield return new WaitForSeconds(repositionTime);
        }
    }

    IEnumerator SwitchGameObjects()
    {
        while (true)
        {
            ActivateCurrentGameObject();

            countdown = switchInterval;

            while (countdown > 0)
            {
                yield return null;
            }

            if (CanActivateThirdObject() && currentObjectIndex == 1)
            {
                currentObjectIndex = 2;
            }
            else
            {
                currentObjectIndex = (currentObjectIndex + 1) % (CanActivateThirdObject() ? 3 : 2);
            }
        }
    }

    private bool CanActivateThirdObject()
    {
        return GetCurrentLevel() >= levelThresholdForThirdObject;
    }

    private void ActivateCurrentGameObject()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == currentObjectIndex);
        }
    }

    private void ChooseNewPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 offset = randomDirection * followRadius;
        targetPosition = (Vector2)player.position + offset;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector2 direction = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private int GetCurrentLevel()
    {
        return invoc.heraStatus;
    }

    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, followRadius);
        }
    }
}
