using System.Collections.Generic;
using UnityEngine;

public class ChainLightningWeapon : MonoBehaviour
{
    public float maxDistance = 10f; // Distance maximale pour attaquer un ennemi
    public int damageAmount = 10; 

    public float chainDistance = 5f; // Distance maximale entre deux ennemis pour propager l'attaque
    public float damageInterval = 0.5f; // Intervalle de temps entre chaque application de dégâts
    public LineRenderer lineRendererPrefab; // Préfabriqué pour afficher la ligne entre le joueur et l'ennemi
    public LayerMask enemyLayer; // Couche des ennemis à détecter
    public saveSytem save;
    private float timeSinceLastDamage = 0f;
    private List<LineRenderer> activeLines = new List<LineRenderer>();
    private List<Transform> enemiesInRange = new List<Transform>();
    private HashSet<Transform> damagedEnemies = new HashSet<Transform>();
    public ParticleSystem noEnemy;
    float Chronomater = 0f;
    public int critChance = 15;
    public float critDamage = 200f;
    public bool isCritical;


    public AudioSource audioSFX;
    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            damageInterval = save.PlayerAttackSpeed;
        if (save.PlayerCritchance != 0)
            critChance = save.PlayerCritchance;
        else
            critChance = 15;
        if (save.PlayerCritDamages != 0)
            critDamage = save.PlayerCritDamages;

       
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Chronomater -= Time.deltaTime;

            timeSinceLastDamage += Time.deltaTime;

            // Mise à jour des lignes en continu
            UpdateLinesAndDamage();

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

    void UpdateLinesAndDamage()
    {
        ClearLines();
        enemiesInRange.Clear(); // Réinitialiser la liste des ennemis

        // Trouver tous les ennemis à proximité du joueur
        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(transform.position, maxDistance, enemyLayer);

        foreach (Collider2D enemy in enemiesDetected)
        {
            Transform enemyTransform = enemy.transform;
            enemiesInRange.Add(enemyTransform);

            DrawLines2(enemy.transform);
            DrawLine(transform.position, enemyTransform.position);
        }
        if(enemiesDetected.Length == 0 && Chronomater <= 0f)
        {
            noEnemy.Play();
            audioSFX.Play();
            Chronomater = 1f;
        }

        // Initialiser la liste des ennemis endommagés pour éviter les répétitions
        damagedEnemies.Clear();
    }

    void ApplyDamage()
    {
        if (enemiesInRange.Count > 0)
        {

            foreach (Transform enemy in enemiesInRange)
            {
                if (!damagedEnemies.Contains(enemy))
                {
                    // Infliger des dégâts à l'ennemi
                    if (enemy.gameObject.CompareTag("Enemy"))
                    {
                        if (Random.Range(1, 100) <= critChance)
                        {
                            damageAmount = (int)(damageAmount * (critDamage / 100f));
                            isCritical = true;
                        }
                        enemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount);
                        DamagePopUp.Create(enemy.transform.position, damageAmount, isCritical);
                        resetDamage();

                    }
                    else if (enemy.gameObject.CompareTag("Boss"))
                    {
                        if (Random.Range(1, 100) <= critChance)
                        {
                            damageAmount = (int)(damageAmount * (critDamage / 100f));
                            isCritical = true;
                        }
                        enemy.GetComponent<Boss1Health>().TakeDamage(damageAmount);
                        DamagePopUp.Create(enemy.transform.position, damageAmount, isCritical);
                        resetDamage();

                    }
                    damagedEnemies.Add(enemy);
                    ChainDamageAndDrawLines(enemy.transform, new List<Transform> { enemy.transform });
                }
            }
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


                if(nearbyEnemy.gameObject.CompareTag("Enemy"))
                {
                    nearbyEnemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount/nearbyEnemies.Length);

                }
                else if (nearbyEnemy.gameObject.CompareTag("Boss"))
                {
                    nearbyEnemy.GetComponent<Boss1Health>().TakeDamage(damageAmount/nearbyEnemies.Length);

                }

                ChainDamageAndDrawLines(nearbyEnemy.transform, damagedEnemies);
            }
        }
    }
    void DrawLines2(Transform currentEnemy)
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(currentEnemy.position, chainDistance, enemyLayer);
        foreach (Collider2D nearbyEnemy in nearbyEnemies)
        {
            DrawLine(currentEnemy.position, nearbyEnemy.transform.position);
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
        foreach (LineRenderer line in activeLines)
        {
            Destroy(line.gameObject);
        }
        activeLines.Clear();
    }
    void resetDamage()
    {
        damageAmount = 8;
        isCritical = false;
    }
}
