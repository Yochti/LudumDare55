using UnityEngine;

public class PointIndicatr : MonoBehaviour
{
    public GameObject pointPrefab; // Préfabriqué du point rouge
    public float spawnDistance = 10f; // Distance à laquelle le point rouge sera placé par rapport au bord de l'écran

    void Update()
    {
        // Trouver tous les ennemis dans la scène
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Vector3 playerPosition = transform.position;

            Vector3 screenPoint = new Vector3(0.5f, 0.5f, 0f);
            Vector3 worldPoint = Camera.main.ViewportToWorldPoint(screenPoint);
            Vector3 direction = worldPoint - playerPosition;
            Vector3 spawnPosition = playerPosition + direction.normalized * spawnDistance;

            Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
