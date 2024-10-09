using System.Collections.Generic;
using UnityEngine;

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
    public bool isCritical;

    private void Start()
    {
        if (save.PlayerAttackSpeed != 0)
            attackSpeed = 0.1f + save.PlayerAttackSpeed;
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
            StartCharging();
        }
        else if (isCharging)
        {
            Fire();
        }
    }

    void StartCharging()
    {
        isCharging = true;
        currentChargeTime += Time.deltaTime;
        float realTime = maxChargeTime - attackSpeed;
        if (currentChargeTime > realTime)
        {
            currentChargeTime = realTime;
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

        currentChargeTime = 0f;
    }

    void ClearLine()
    {
        if (currentLineRenderer != null)
        {
            Destroy(currentLineRenderer.gameObject);
            currentLineRenderer = null;
        }
    }
}
