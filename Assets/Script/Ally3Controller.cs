using UnityEngine;

public class Ally3Controller : MonoBehaviour
{
    public GameObject mineTrapPrefab;
    public GameObject attractionTrapPrefab;
    public GameObject damageTrapPrefab; // Nouveau piège de dégâts
    public float trapPlacementInterval = 10f;
    private float lastTrapPlacementTime;
    private AlliesSummon alliesSummon; // Script pour vérifier le niveau de l'allié

    public float moveSpeed = 4f;
    public float orbitRadius = 7f;
    public float orbitSpeed = 2f;
    private Vector3 playerPosition;

    void Start()
    {
        lastTrapPlacementTime = Time.time;
        alliesSummon = FindObjectOfType<AlliesSummon>(); 
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
       if (alliesSummon.junktrapStatus == "1" || alliesSummon.junktrapStatus == "2") 
       {
            GameObject[] trapPrefabs = { mineTrapPrefab, attractionTrapPrefab };
            int randomIndex = Random.Range(0, trapPrefabs.Length);
            Vector2 trapPosition = transform.position + transform.forward * 2f;
            Instantiate(trapPrefabs[randomIndex], trapPosition, Quaternion.identity);
       }
       else
       {
            GameObject[] trapPrefabs = { mineTrapPrefab, attractionTrapPrefab, damageTrapPrefab }; // Ajouter le nouveau piège
            int randomIndex = Random.Range(0, trapPrefabs.Length);
            Vector2 trapPosition = transform.position + transform.forward * 2f;
            Instantiate(trapPrefabs[randomIndex], trapPosition, Quaternion.identity);
       }
    }
}
