using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageEnemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Vitesse de d�placement
    public float attackRange = 2f; // Port�e d'attaque
    public float illusionSpawnInterval = 5f; // Intervalle de temps pour cr�er des illusions
    public int illusionDamage = 5; // D�g�ts inflig�s au joueur lorsque l'illusion est d�truite
    public int attackDamage = 25; // D�g�ts inflig�s lors de l'attaque
    public GameObject illusionPrefab; // Pr�fabriqu� de l'illusion
    public int maxIllusions = 4; // Nombre maximum d'illusions actives
    private List<GameObject> activeIllusions = new List<GameObject>();
    private Transform player;
    private float timeSinceLastIllusion;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastIllusion = 5f;
    }

    void Update()
    {
        MoveTowardsPlayer();

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

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
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
