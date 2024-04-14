using UnityEngine;

public class Ally3Controller : MonoBehaviour
{
    public GameObject mineTrapPrefab;
    public GameObject attractionTrapPrefab;
    public float trapPlacementInterval = 10f;
    private float lastTrapPlacementTime;

    public float moveSpeed = 3f;
    public float orbitRadius = 5f;
    public float orbitSpeed = 2f;
    private Vector3 playerPosition;

    void Start()
    {
        lastTrapPlacementTime = Time.time;
    }

    void Update()
    {
        MoveAroundPlayer();

        if (Time.time - lastTrapPlacementTime >= trapPlacementInterval)
        {
            PlaceRandomTrap();
            lastTrapPlacementTime = Time.time;
        }
    }

    void MoveAroundPlayer()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        float angle = Time.time * orbitSpeed;
        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * orbitRadius;
        Vector3 targetPosition = playerPosition + offset;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        transform.up = playerPosition - transform.position;
    }

    void PlaceRandomTrap()
    {
        GameObject trapPrefab = Random.value < 0.5f ? mineTrapPrefab : attractionTrapPrefab;
        Vector3 trapPosition = transform.position + transform.forward * 2f;
        Instantiate(trapPrefab, trapPosition, Quaternion.identity);
    }
}
