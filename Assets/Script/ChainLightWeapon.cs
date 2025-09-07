using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
public class ChainLightningWeapon : MonoBehaviour
{
    public float maxDistance = 10f; 
    private int damageAmount;
    public int damageAmountmin = 10;
    public int damageAmountmax = 18;

    public float chainDistance = 5f; // Distance maximale entre deux ennemis pour propager l'attaque
    public float damageInterval = 0.5f; // Intervalle de temps entre chaque application de dégâts
    public LineRenderer lineRendererPrefab; // Préfabriqué pour afficher la ligne entre le joueur et l'ennemi
    public LayerMask enemyLayer; // Couche des ennemis à détecter
    public saveSytem save;

    // Variables pour l'auto-aim et l'auto-shoot
    public bool autoAim = false; // Activer/désactiver l'auto-aim
    public bool autoShoot = false; // Activer/désactiver l'auto-shoot
    public float detectionRadius = 10f; // Rayon de détection pour l'auto-aim
    public Transform player; // Transform du joueur pour la rotation

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
    public float baseAttackSpeed;
    private void Start()
    {


        StartCoroutine(waitingForAwake());

        critChance = PlayerStats.critRate;
        critDamage = PlayerStats.critDamage;

    }

    void Update()
    {
        damageAmount = Random.Range(
            Mathf.FloorToInt(damageAmountmin * (1 + PlayerStats.additionalDamage)),
            Mathf.FloorToInt(damageAmountmax * (1 + PlayerStats.additionalDamage)) + 1
        );
        damageInterval = baseAttackSpeed - (PlayerStats.attackSpeed * damageInterval * 0.5f);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        Transform target = autoAim ? FindTarget() : null;
        if (autoAim && target != null)
            AimAtTarget(target);
        if (autoShoot && (target != null || !autoAim) && target != null)
        {
            Chronomater -= Time.deltaTime;

            timeSinceLastDamage += Time.deltaTime;

            UpdateLinesAndDamage();

            if (timeSinceLastDamage >= damageInterval)
            {
                timeSinceLastDamage = 0f;
                ApplyDamage();
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            Chronomater -= Time.deltaTime;

            timeSinceLastDamage += Time.deltaTime;

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
        enemiesInRange.Clear();

        Collider2D[] enemiesDetected = Physics2D.OverlapCircleAll(transform.position, maxDistance, enemyLayer);

        foreach (Collider2D enemy in enemiesDetected)
        {
            Transform enemyTransform = enemy.transform;
            enemiesInRange.Add(enemyTransform);

            DrawLines2(enemy.transform);
            DrawLine(transform.position, enemyTransform.position);
        }

        if (enemiesDetected.Length == 0 && Chronomater <= 0f)
        {
            noEnemy.Play();
            audioSFX.Play();
            Chronomater = 1f;
        }

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
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(currentEnemy.position, chainDistance, enemyLayer);

        foreach (Collider2D nearbyEnemy in nearbyEnemies)
        {
            if (!damagedEnemies.Contains(nearbyEnemy.transform))
            {
                // Ajouter cet ennemi à la liste des ennemis déjà endommagés
                damagedEnemies.Add(nearbyEnemy.transform);

                if (nearbyEnemy.gameObject.CompareTag("Enemy"))
                {
                    nearbyEnemy.GetComponent<EnemmiHealth>().TakeDamage(damageAmount / nearbyEnemies.Length);
                }
                else if (nearbyEnemy.gameObject.CompareTag("Boss"))
                {
                    nearbyEnemy.GetComponent<Boss1Health>().TakeDamage(damageAmount / nearbyEnemies.Length);
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

    IEnumerator waitingForAwake()
    {
        yield return new WaitForSeconds(0.0001f);
    }

    private Transform FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        Transform closestTarget = hits
            .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
            .Select(hit => hit.transform)
            .FirstOrDefault();

        return closestTarget;
    }

    private void AimAtTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        player.rotation = Quaternion.Euler(0, 0, angle); // Oriente le joueur vers la cible
    }
}
