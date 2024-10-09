using UnityEngine;

public class BossDropSoulCollect : MonoBehaviour
{
    public GameObject soulPrefab;
    public int soulAmount = 1;

    private void OnDestroy()
    {
        GenerateSouls();
    }

    void GenerateSouls()
    {
        for (int i = 0; i < soulAmount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
            Vector3 soulPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            GameObject soul = Instantiate(soulPrefab, soulPosition, Quaternion.identity);
        }
    }
}

