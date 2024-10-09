using UnityEngine;

public class Ally5Controller : MonoBehaviour
{
    public GameObject wallPrefab;
    public float placementInterval = 5f;
    public float placementDistance = 2f;
    public float moveSpeed = 5f; 
    public float wallLifetime = 10f; 

    private Transform playerTransform; 

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Finding the player's transform
        InvokeRepeating("PlaceWall", 0f, placementInterval); // Start placing walls at regular intervals
    }

    void PlaceWall()
    {
        Vector3 wallPosition = transform.position + transform.forward * placementDistance; // Calculate the position in front of the ally
        Quaternion wallRotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x) * Mathf.Rad2Deg - 90); // Calculate the rotation perpendicular to the player's direction
        GameObject wall = Instantiate(wallPrefab, wallPosition, wallRotation); // Instantiate the wall prefab with the calculated position and rotation
        Destroy(wall, wallLifetime); 
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > placementDistance)
        {
            Vector3 moveDirection = playerTransform.position - transform.position; // Calculate the direction to move towards the player
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime; // Move towards the player with increased speed
        }
    }
}
