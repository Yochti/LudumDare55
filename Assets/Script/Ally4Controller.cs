using UnityEngine;

public class Ally4Controller : MonoBehaviour
{
    public GameObject bulletTurretPrefab; 
    public GameObject healTurretPrefab;
    public Transform player;

    public float moveSpeed = 2f;
    public float wanderRadius = 10f;
    public float wanderTimer = 10f; 
    public float turretPlacementDelay = 5f; 

    private float timer; 
    private float wanderTime; 
    private Vector3 wanderPosition; 

    private int maxTurrets = 3; 
    private int currentTurrets = 0; 

    private void Start()
    {
        timer = turretPlacementDelay;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > wanderRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        }
        else
        {
            if (wanderTime <= 0)
            {
                wanderPosition = player.position + Random.insideUnitSphere * wanderRadius;
                wanderTime = wanderTimer;
            }
            else
            {
                wanderTime -= Time.deltaTime;
            }

            transform.position = Vector3.MoveTowards(transform.position, wanderPosition, moveSpeed * Time.deltaTime);
        }

        if (timer <= 0)
        {
            GameObject turretPrefab = Random.value < 0.5f ? bulletTurretPrefab : healTurretPrefab;

            if (currentTurrets < maxTurrets)
            {
                PlaceTurret(turretPrefab);
            }
            else
            {
                
                ReplaceOldestTurretAndPlaceNew(turretPrefab);
            }

            timer = turretPlacementDelay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void PlaceTurret(GameObject turretPrefab)
    {
        Instantiate(turretPrefab, transform.position, Quaternion.identity);
        currentTurrets++;
    }

    private void ReplaceOldestTurretAndPlaceNew(GameObject newTurretPrefab)
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        if (turrets.Length > 0)
        {
            Destroy(turrets[0]);
            PlaceTurret(newTurretPrefab);
        }
    }
}
