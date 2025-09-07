using UnityEngine;
using TMPro;

public class tooltipManager : MonoBehaviour
{
    public static tooltipManager Instance;

    public GameObject tooltipObject;
    public TextMeshProUGUI tooltipText;

    private bool isShowing = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isShowing)
        {
            Vector2 mousePos = Input.mousePosition;
            tooltipObject.transform.position = mousePos + new Vector2(10f, -130f); 
        }
    }

    public void ShowTooltip(string message, Vector3 position)
    {
        tooltipText.text = message;
        tooltipObject.SetActive(true);
        isShowing = true;
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
        isShowing = false;
    }
}
