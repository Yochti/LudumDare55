using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleEnemyZacAI : MonoBehaviour
{
    public float moveSpeed = 2f; // Vitesse de déplacement vers un autre petit ennemi
    public float fusionDistance = 0.5f; // Distance à laquelle les petits ennemis se fusionnent
    public GameObject bigEnemyPrefab; // Préfabriqué du grand ennemi
    public static int smallEnemiesCount = 0; // Compte le nombre de petits ennemis restants
    public int minSmallEnemiesForBigEnemy = 2; // Nombre minimum d'ennemis pour recréer le grand
    private Transform targetEnemy; // Cible du petit ennemi pour se rejoindre

    void Start()
    {
        smallEnemiesCount++;
    }

    void Update()
    {
        FindClosestSmallEnemy();

        if (targetEnemy != null)
        {
            MoveTowardsTarget();

            if (Vector2.Distance(transform.position, targetEnemy.position) < fusionDistance)
            {
                FusionWithOtherSmallEnemy();
            }
        }
    }

    void FindClosestSmallEnemy()
    {
        littleEnemyZacAI[] allSmallEnemies = FindObjectsOfType<littleEnemyZacAI>();
        float closestDistance = Mathf.Infinity;

        foreach (littleEnemyZacAI enemy in allSmallEnemies)
        {
            if (enemy != this) // Ne pas se cibler soi-même
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetEnemy = enemy.transform;
                }
            }
        }

        // Si aucun autre petit ennemi n'est trouvé, ne pas bouger
        if (targetEnemy == null)
        {
            StopMoving();
        }
    }

    void MoveTowardsTarget()
    {
        // Se déplacer vers l'autre petit ennemi
        transform.position = Vector2.MoveTowards(transform.position, targetEnemy.position, moveSpeed * Time.deltaTime);
    }

    void StopMoving()
    {
        targetEnemy = null;
    }

    void FusionWithOtherSmallEnemy()
    {
        smallEnemiesCount -= 2;

        Destroy(targetEnemy.gameObject);
        Destroy(gameObject);

        if (smallEnemiesCount <= minSmallEnemiesForBigEnemy)
        {
            RespawnBigEnemy();
        }
    }

    void RespawnBigEnemy()
    {
        Instantiate(bigEnemyPrefab, transform.position, Quaternion.identity);

        smallEnemiesCount = 0;
    }
}
