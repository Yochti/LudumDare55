using UnityEngine;

public class BossBehavioure : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float rotationRadius = 7f;
    public GameObject poisonPrefab;
    public Transform player;
    public int numberOfPoison = 7;
    private bool isRotating = false;
    private float rotationTimer = 0.05f;
    private Vector3 playerPosition;
    private Boss1Health bossHp;
    private EnemyWaveManager waveManager;
    private void Start()
    {
        bossHp = GetComponent<Boss1Health>();
        waveManager = FindObjectOfType<EnemyWaveManager>();

    }
    private void Update()
    {     
            RotateAroundPlayer();
            SpawnPoison();
        
    }

    /*private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }*/

    private void RotateAroundPlayer()
    {
        
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        float angle = Time.time * rotationSpeed;
        Vector3 offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * rotationRadius;
        Vector3 targetPosition = playerPosition + offset;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        transform.up = playerPosition - transform.position;
    }

    private void SpawnPoison()
    {
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0)
        {
            for (int i = 0; i < numberOfPoison; i++)
            {
                Instantiate(poisonPrefab, transform.position, Quaternion.identity);
            }
            rotationTimer = 0.1f;
        }
    }
    
}
