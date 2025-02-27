using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SniperWeapon : MonoBehaviour
{
    public float maxChargeTime = 3f; // Temps maximal pour charger la balle
    public int baseDamage = 10; // Dégâts de base
    public int maxDamage = 50;
    public float maxRange = 50f; // Portée maximale du sniper
    public LineRenderer lineRendererPrefab; // Préfabriqué pour afficher le rayon
    public LayerMask enemyLayer; // Couche des ennemis à détecter
    public saveSytem save;
    public int critChance = 15;
    public float critDamage = 200f;
    public float attackSpeed;
    private float currentChargeTime = 0f;
    private bool isCharging = false;
    private LineRenderer currentLineRenderer;
    public AudioSource Audio;
    public AudioSource Audio1;
    public bool isCritical;
    private int y;
    public Transform player;

    public bool autoAim = false; // Activer/désactiver l'auto-aim
    public bool autoShoot = false; // Activer/désactiver l'auto-shoot
    public float detectionRadius = 15f; // Rayon de détection pour auto-aim
    private float baseAttackSpeed;
    private void Start()
    {

        baseAttackSpeed = PlayerStats.attackSpeed;


    }

    void Update()
    {
        critChance = PlayerStats.critRate;
        critDamage = PlayerStats.critDamage;
        
        attackSpeed = baseAttackSpeed - (PlayerStats.attackSpeed * attackSpeed * 0.5f);
        autoShoot = save.isAutoShoot;
        autoAim = save.isAutoAim;
        Transform target = autoAim ? FindTarget() : null;


        if (autoAim && target != null) AimAtTarget(target);
        if (autoShoot && (target != null || !autoAim) )
        {
            if (currentChargeTime >= (maxChargeTime - attackSpeed))
                Fire();
            else
                StartCharging();
                
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Si l'input est maintenu, charger
            {
                StartCharging();
            }
            else if (isCharging)
            {
                Fire();
            }
        }
         
    }

    private Transform FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        Transform closestTarget = hits
            .Where(hit => hit.CompareTag("Enemy") || hit.CompareTag("Boss"))
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

    void StartCharging()
    {
        isCharging = true;
        currentChargeTime += Time.deltaTime;
        float realTime = maxChargeTime - attackSpeed;

        if (currentChargeTime > realTime)
        {
            currentChargeTime = realTime;
            if (y == 0)
            {
                Audio1.Play();
                y++;
            }
        }

        if (currentLineRenderer == null)
        {
            currentLineRenderer = Instantiate(lineRendererPrefab);
        }

        // Calculer la largeur du rayon en fonction du temps de charge
        float lineWidth = Mathf.Lerp(0.05f, 0.2f, currentChargeTime / realTime);
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;

        // Calculer la transparence du rayon en fonction du temps de charge
        Color color = currentLineRenderer.startColor;
        color.a = Mathf.Lerp(0.3f, 1f, currentChargeTime / realTime);
        currentLineRenderer.startColor = color;
        currentLineRenderer.endColor = color;

        // Dessiner le rayon vers l'avant
        Vector3 endPosition = transform.position + transform.up * maxRange; // Utiliser transform.up pour la direction du rayon
        currentLineRenderer.SetPosition(0, transform.position);
        currentLineRenderer.SetPosition(1, endPosition);
    }

    void Fire()
    {
        Audio.Play();
        y = 0;
        isCharging = false;

        int damage = Mathf.FloorToInt(Mathf.Lerp(baseDamage, maxDamage, currentChargeTime / (maxChargeTime - attackSpeed)));
        if (Random.Range(1, 100) <= critChance)
        {
            damage = (int)(damage * (critDamage / 100f));
            isCritical = true;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxRange, enemyLayer);

        if (hit.collider != null && hit.collider.GetComponent<EnemmiHealth>() != null)
        {
            hit.collider.GetComponent<EnemmiHealth>().TakeDamage(damage);
            DamagePopUp.Create(hit.transform.position, damage, isCritical);
            isCritical = false;
        }
        else if (hit.collider != null && hit.collider.GetComponent<Boss1Health>() != null)
        {
            hit.collider.GetComponent<Boss1Health>().TakeDamage(damage);
            DamagePopUp.Create(hit.transform.position, damage, isCritical);
            isCritical = false;
        }

        Color color = currentLineRenderer.startColor;
        color.a = 1f; // Opacité totale lors du tir
        currentLineRenderer.startColor = color;
        currentLineRenderer.endColor = color;

        Invoke(nameof(ClearLine), 0.2f);

        currentChargeTime = 0f; // Réinitialise le temps de charge après le tir
    }

    void ClearLine()
    {
        if (currentLineRenderer != null)
        {
            Destroy(currentLineRenderer.gameObject);
            currentLineRenderer = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
