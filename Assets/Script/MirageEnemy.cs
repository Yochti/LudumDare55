using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageEnemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de déplacement
    public float attackRange = 2f; // Portée d'attaque
    public float illusionSpawnInterval = 5f; // Intervalle de temps pour créer des illusions
    public int illusionDamage = 5; // Dégâts infligés au joueur lorsque l'illusion est détruite
    public int attackDamage = 25; // Dégâts infligés lors de l'attaque
    public GameObject illusionPrefab; // Préfabriqué de l'illusion
    public int maxIllusions = 4; // Nombre maximum d'illusions actives
    private List<GameObject> activeIllusions = new List<GameObject>();
    private Transform player;
    private float timeSinceLastIllusion;
    private Vector3 targetPosition;
    public float detectionRadius = 8f;
    private Transform poutch;
    private EnemyC enemyC;

    void Start()
    {
        enemyC = this.gameObject.GetComponent<EnemyC>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastIllusion = 5f;
    }

    void Update()
    {
        moveSpeed = enemyC.currentSpeed;
        if (poutch == null)
        {
            GameObject poutchObject = GameObject.FindGameObjectWithTag("Poutch");
            if (poutchObject != null)
            {
                poutch = poutchObject.transform;
            }
        }
        if(poutch != null)
        {
            MoveTowardsPlayer(poutch);
        }
        else if (!PlayerInvisibleZone.isInvisible)
            MoveTowardsPlayer(player);
        else
            MoveTowardsApproximatePosition();

        timeSinceLastIllusion += Time.deltaTime;
        if (timeSinceLastIllusion >= illusionSpawnInterval)
        {
            SpawnIllusions();
            timeSinceLastIllusion = 0f;
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    void MoveTowardsPlayer(Transform target)
    {
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        
    }
    private void MoveTowardsApproximatePosition()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f || targetPosition == Vector3.zero)
        {
            targetPosition = GetRandomPointAround(PlayerInvisibleZone.lastKnownPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed); 
    }

    private Vector3 GetRandomPointAround(Vector3 center)
    {
        Vector2 randomPoint = Random.insideUnitCircle * detectionRadius;
        return new Vector3(center.x + randomPoint.x, center.y + randomPoint.y, transform.position.z);
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }

    void SpawnIllusions()
    {
        int illusionsToSpawn = maxIllusions - activeIllusions.Count;;

        for (int i = 0; i < illusionsToSpawn; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 5f; 
            spawnPosition.z = 0;
            GameObject illusion = Instantiate(illusionPrefab, spawnPosition, Quaternion.identity);
            illusion.GetComponent<Illusion>().SetParentEnemy(this);
            activeIllusions.Add(illusion);

        }
    }
    public void RemoveIllusion(GameObject illusion)
    {
        if (activeIllusions.Contains(illusion))
        {
            activeIllusions.Remove(illusion);
        }
    }
}
