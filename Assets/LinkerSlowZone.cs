using System.Collections.Generic;
using UnityEngine;

public class LinkerSlowZone : MonoBehaviour
{
    private Transform[] linkerPoints;
    public float slowFactor = 0.7f;

    private EnemyC[] enemies;

    public void Initialize(Transform[] points)
    {
        linkerPoints = points;
    }

    void Update()
    {
        if (linkerPoints == null || linkerPoints.Length < 3) return;

        enemies = GameObject.FindObjectsOfType<EnemyC>();

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            Vector3 pos = enemy.transform.position;
            if (IsPointInTriangle(pos, linkerPoints[0].position, linkerPoints[1].position, linkerPoints[2].position))
            {
                enemy.ApplySlow(slowFactor);
            }
            else
            {
                enemy.ResetSpeed();
            }
        }
    }

    bool IsPointInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        float sign(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
        }

        bool b1 = sign(p, a, b) < 0.0f;
        bool b2 = sign(p, b, c) < 0.0f;
        bool b3 = sign(p, c, a) < 0.0f;

        return (b1 == b2) && (b2 == b3);
    }
}
