using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton pour acc�der au UIManager

    [SerializeField] private GameObject powerPanel; // Panel d�sactiv�
    public PowerUpPanel powerup;
    private void Awake()
    {
        // On v�rifie qu'il n'y a pas d'autre instance du singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPowerPanel()
    {
        if (powerPanel != null)
        {
            powerPanel.SetActive(true);
            
            powerup.ShowRandomPowerUps();
        }
        else
        {
            Debug.LogError("Power Panel is not assigned in the UIManager.");
        }
    }
}
