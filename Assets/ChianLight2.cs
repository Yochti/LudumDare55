using System.Collections.Generic;
using UnityEngine;

public class chainLightn2 : MonoBehaviour
{
    public float maxDistance = 10f; // Distance maximale pour attaquer un ennemi
    public int damageAmount = 10; // D�g�ts inflig�s par l'arme
    public float chainDistance = 5f; // Distance maximale entre deux ennemis pour propager l'attaque
    public float damageInterval = 0.5f; // Intervalle de temps entre chaque application de d�g�ts
    public LineRenderer lineRendererPrefab; // Pr�fabriqu� pour afficher la ligne entre le joueur et l'ennemi
    public LayerMask enemyLayer; // Couche des ennemis � d�tecter
    private float timeSinceLastDamage = 0f;
    private List<LineRenderer> activeLines = new List<LineRenderer>();
    private List<Transform> currentEnemies = new List<Transform>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timeSinceLastDamage += Time.deltaTime;

            // Mise � jour des lignes en continu
            ApplyDamageAndDrawLines();

            if (timeSinceLastDamage >= damageInterval)
            {
                timeSinceLastDamage = 0f;
                ApplyDamage();
            }
        }
        else
        {
            ClearLines();
        }
    }

    void ApplyDamageAndDrawLines()
    {
        ClearLines();

        // Trouver tous les ennemis � proximit� du joueur
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, maxDistance, enemyLayer);
        currentEnemies.Clear();

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                // Ajouter l'ennemi � la liste actuelle
                currentEnemies.Add(enemy.transform);

                // Dessiner une ligne entre le joueur et cet ennemi
                DrawLine(transform.position, enemy.transform.position);

                // Propager le dessin des lignes et les d�g�ts aux ennemis proches
                ChainDamageAndDrawLines(enemy.transform, new List<Transform> { enemy.transform });
            }
        }
    }

    void ApplyDamage()
    {
        foreach (Transform enemy in currentEnemies)
        {
            // Infliger des d�g�ts � l'ennemi
            enemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
        }
    }

    void ChainDamageAndDrawLines(Transform currentEnemy, List<Transform> damagedEnemies)
    {
        // Rechercher les ennemis � proximit� du dernier ennemi endommag�
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(currentEnemy.position, chainDistance, enemyLayer);

        foreach (Collider2D nearbyEnemy in nearbyEnemies)
        {
            if (!damagedEnemies.Contains(nearbyEnemy.transform))
            {
                // Ajouter cet ennemi � la liste des ennemis d�j� endommag�s
                damagedEnemies.Add(nearbyEnemy.transform);

                // Dessiner une ligne entre les ennemis
                DrawLine(currentEnemy.position, nearbyEnemy.transform.position);

                // Infliger des d�g�ts � cet ennemi
                nearbyEnemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);

                // Continuer � propager les d�g�ts et le dessin des lignes
                //ChainDamageAndDrawLines(nearbyEnemy.transform, damagedEnemies);
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(lineRendererPrefab);
        line.SetPosition(0, start);  // Point de d�part de la ligne
        line.SetPosition(1, end);    // Point d'arriv�e de la ligne
        activeLines.Add(line);
    }

    void ClearLines()
    {
        // Effacer toutes les lignes actives
        foreach (LineRenderer line in activeLines)
        {
            Destroy(line.gameObject);
        }
        activeLines.Clear();
    }
}
