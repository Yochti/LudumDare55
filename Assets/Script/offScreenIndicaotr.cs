using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OffScreenIndicator : MonoBehaviour
{
    public Transform player;
    public Image indicatorImages;
    public float minDistance = 30f;
    private RectTransform canvasRectTransform;
    public TextMeshProUGUI textMeters;

    void Start()
    {
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform closestEnemy = GetClosestEnemy(enemies);

        if (closestEnemy != null)
        {
            float distance = Vector3.Distance(player.position, closestEnemy.position);

            if (distance >= minDistance)
            {
                Vector3 direction = closestEnemy.position - player.position;
                direction.z = 0f; 
                direction.Normalize();

                Vector3 indicatorPosition = player.position + direction * minDistance;
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(indicatorPosition);

                Vector2 clampedScreenPosition = new Vector2(
                    Mathf.Clamp(screenPosition.x, 0f, canvasRectTransform.rect.width),
                    Mathf.Clamp(screenPosition.y, 0f, canvasRectTransform.rect.height)
                );

                clampedScreenPosition.x = Mathf.Clamp(clampedScreenPosition.x, 0f, Screen.width);
                clampedScreenPosition.y = Mathf.Clamp(clampedScreenPosition.y, 0f, Screen.height);

                indicatorImages.rectTransform.position = clampedScreenPosition;

                float textOffsetVertical = 30f; 
                float textOffsetHorizontal = 30f; 
                Vector2 textPosition = clampedScreenPosition;

                if (screenPosition.y < Screen.height / 2)
                {
                    textPosition.y += textOffsetVertical;
                }
                else
                {
                    textPosition.y -= textOffsetVertical;
                }

                if (screenPosition.x < Screen.width / 2)
                {
                    textPosition.x += textOffsetHorizontal;
                }
                else
                {
                    textPosition.x -= textOffsetHorizontal;
                }

                textMeters.rectTransform.position = textPosition;

                textMeters.text = Mathf.Round(distance).ToString() + "m";
                indicatorImages.enabled = true;
                textMeters.gameObject.SetActive(true);
            }
            else
            {
                textMeters.gameObject.SetActive(false);
                indicatorImages.enabled = false;
            }
        }
        else
        {
            indicatorImages.enabled = false;
            textMeters.gameObject.SetActive(false);
        }
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy.transform;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
