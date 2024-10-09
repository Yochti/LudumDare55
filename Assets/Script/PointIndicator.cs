using UnityEngine;

public class PointIndicatr : MonoBehaviour
{
    public GameObject pointPrefab; // Pr�fabriqu� du point rouge
    public float spawnDistance = 10f; // Distance � laquelle le point rouge sera plac� par rapport au bord de l'�cran

    void Update()
    {
        // Trouver tous les ennemis dans la sc�ne
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
