using System.Collections;
using UnityEngine;

public class FlameZoneInstanciate : MonoBehaviour
{
    public GameObject flameZonePrefab;

    public Transform playerTransform;
    public static int number = 3;
    public float spawnRadiusMin = 2f;
    public float spawnRadiusMax = 5f;
    public float fireRate = 2f;  // Temps entre chaque période d'instanciation
    public float spawnGrowthTime = 0.5f;  // Temps pour que la zone atteigne sa taille finale
    public float timeBeetweenProjectile = 0.3f;
    private bool canSpawn = true;


    private void Start()
    {
        number = 0;
    }
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnFlameZone());
        }
    }

    IEnumerator SpawnFlameZone()
    {
        canSpawn = false;

        for (int i = 0; i < number; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            GameObject flameZone = Instantiate(flameZonePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(ShowZoneOutline(flameZone)); // Afficher les contours avant l'apparition de la zone
            yield return new WaitForSeconds(timeBeetweenProjectile);

        }

        yield return new WaitForSeconds(fireRate);
        canSpawn = true;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 playerPosition = playerTransform.position;
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(spawnRadiusMin, spawnRadiusMax);
        Vector2 spawnPosition = playerPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return spawnPosition;
    }

    IEnumerator ShowZoneOutline(GameObject flameZone)
    {
        SpriteRenderer zoneRenderer = flameZone.GetComponent<SpriteRenderer>();
        zoneRenderer.enabled = false; // Ne pas montrer la zone complète au début

        // Récupérer l'enfant "contour" et le montrer avant la zone complète
        Transform outlineTransform = flameZone.transform.Find("Contour");
        if (outlineTransform != null)
        {
            SpriteRenderer outlineRenderer = outlineTransform.GetComponent<SpriteRenderer>();
        }

        // Après un petit délai, faire grandir la zone et cacher le contour
        yield return new WaitForSeconds(0.7f);
        if (outlineTransform != null)
        {
            outlineTransform.gameObject.SetActive(false);
        }
        StartCoroutine(GrowFlameZone(flameZone));

        
    }

    IEnumerator GrowFlameZone(GameObject flameZone)
    {
        SpriteRenderer zoneRenderer = flameZone.GetComponent<SpriteRenderer>();
        zoneRenderer.enabled = true; // La zone est invisible au début

        Vector3 initialScale = Vector3.zero; 
        Vector3 finalScale = new Vector3(4f, 4f, 1f); 
        float elapsedTime = 0f;

        // Croissance de la zone
        while (elapsedTime < spawnGrowthTime)
        {
            flameZone.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / spawnGrowthTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flameZone.transform.localScale = finalScale; // Assurer que la taille finale est atteinte
        zoneRenderer.enabled = true; // Montrer la zone une fois la croissance terminée
    }
}
