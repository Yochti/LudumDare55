using System.Collections.Generic;
using UnityEngine;

public class chainLightn2 : MonoBehaviour
{
    public float maxDistance = 10f; // Distance maximale pour attaquer un ennemi
    public int damageAmount = 10; // Dégâts infligés par l'arme
    public float chainDistance = 5f; // Distance maximale entre deux ennemis pour propager l'attaque
    public float damageInterval = 0.5f; // Intervalle de temps entre chaque application de dégâts
    public LineRenderer lineRendererPrefab; // Préfabriqué pour afficher la ligne entre le joueur et l'ennemi
    public LayerMask enemyLayer; // Couche des ennemis à détecter
    private float timeSinceLastDamage = 0f;
    private List<LineRenderer> activeLines = new List<LineRenderer>();
    private List<Transform> currentEnemies = new List<Transform>();

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timeSinceLastDamage += Time.deltaTime;

            // Mise à jour des lignes en continu
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

        // Trouver tous les ennemis à proximité du joueur
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, maxDistance, enemyLayer);
        currentEnemies.Clear();

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                // Ajouter l'ennemi à la liste actuelle
                currentEnemies.Add(enemy.transform);

                // Dessiner une ligne entre le joueur et cet ennemi
                DrawLine(transform.position, enemy.transform.position);

                // Propager le dessin des lignes et les dégâts aux ennemis proches
                ChainDamageAndDrawLines(enemy.transform, new List<Transform> { enemy.transform });
            }
        }
    }

    void ApplyDamage()
    {
        foreach (Transform enemy in currentEnemies)
        {
            // Infliger des dégâts à l'ennemi
            enemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
        }
    }

    void ChainDamageAndDrawLines(Transform currentEnemy, List<Transform> damagedEnemies)
    {
        // Rechercher les ennemis à proximité du dernier ennemi endommagé
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(currentEnemy.position, chainDistance, enemyLayer);

        foreach (Collider2D nearbyEnemy in nearbyEnemies)
        {
            if (!damagedEnemies.Contains(nearbyEnemy.transform))
            {
                // Ajouter cet ennemi à la liste des ennemis déjà endommagés
                damagedEnemies.Add(nearbyEnemy.transform);

                // Dessiner une ligne entre les ennemis
                DrawLine(currentEnemy.position, nearbyEnemy.transform.position);

                // Infliger des dégâts à cet ennemi
                nearbyEnemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);

                // Continuer à propager les dégâts et le dessin des lignes
                //ChainDamageAndDrawLines(nearbyEnemy.transform, damagedEnemies);
            }
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        LineRenderer line = Instantiate(lineRendererPrefab);
        line.SetPosition(0, start);  // Point de départ de la ligne
        line.SetPosition(1, end);    // Point d'arrivée de la ligne
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
