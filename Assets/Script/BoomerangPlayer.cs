using UnityEngine;

public class BoomerangPlayer : MonoBehaviour
{
    public Boomerang boomerangPrefab;  // Le prefab du boomerang
    public Transform boomerangSpawnPoint; // Le point de spawn du boomerang

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            // Instancier le boomerang au point de spawn
            Boomerang boomerang = Instantiate(boomerangPrefab, boomerangSpawnPoint.position, Quaternion.identity);

            // Calculer la direction dans laquelle le joueur regarde
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - boomerangSpawnPoint.position;

            boomerang.Initialize(direction, this.transform);
        }
    }
}
