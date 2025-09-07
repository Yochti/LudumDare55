using System.Collections.Generic;
using UnityEngine;

public class LinkerLineRay : MonoBehaviour
{
    public LinkerManager manager;
    public float stunDuration = 0.3f;
    public float stunRange = 0.5f; // distance max entre l'ennemi et la ligne
    public LayerMask enemyLayer;

    private Dictionary<GameObject, float> stunCooldowns = new Dictionary<GameObject, float>();
    public float cooldownBetweenStuns = 3f;
    private void Start()
    {
        stunDuration = 0.35f;
        cooldownBetweenStuns = 2f;        
    }
    void Update()
    {
        if (manager == null) return;

        Transform[] parts = manager.GetLinkerParts();
        if (parts[0] == null || parts[1] == null) return;

        CheckAndStunEnemies(parts[0].position, parts[1].position);

        if (manager.hasThirdNode && parts[2] != null)
        {
            CheckAndStunEnemies(parts[1].position, parts[2].position);
            CheckAndStunEnemies(parts[2].position, parts[0].position);
        }

        UpdateCooldowns();
    }

    void CheckAndStunEnemies(Vector3 a, Vector3 b)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll((a + b) / 2, Vector3.Distance(a, b) / 2f + stunRange, enemyLayer);

        foreach (var col in enemies)
        {
            GameObject enemy = col.gameObject;
            if (IsNearLine(enemy.transform.position, a, b, stunRange))
            {
                if (!stunCooldowns.ContainsKey(enemy) || stunCooldowns[enemy] <= 0f)
                {
                    EnemyC enemyScript = enemy.GetComponent<EnemyC>();
                    if (enemyScript != null)
                    {
                        enemyScript.Stun(stunDuration);
                        stunCooldowns[enemy] = cooldownBetweenStuns;
                    }
                }
            }
        }
    }

    bool IsNearLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd, float threshold)
    {
        float distance = Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
        return distance <= threshold;
    }

    Vector3 ProjectPointLine(Vector3 point, Vector3 a, Vector3 b)
    {
        Vector3 ap = point - a;
        Vector3 ab = b - a;
        float magnitudeAB = ab.sqrMagnitude;
        float abAPProduct = Vector3.Dot(ap, ab);
        float distance = abAPProduct / magnitudeAB;
        return a + ab * distance;
    }

    void UpdateCooldowns()
    {
        List<GameObject> keys = new List<GameObject>(stunCooldowns.Keys);
        foreach (var key in keys)
        {
            stunCooldowns[key] -= Time.deltaTime;
            if (stunCooldowns[key] <= 0f)
                stunCooldowns.Remove(key);
        }
    }
}
