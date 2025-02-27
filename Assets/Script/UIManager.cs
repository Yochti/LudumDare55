using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton pour accéder au UIManager

    [SerializeField] private GameObject powerPanel; // Panel désactivé
    public PowerUpPanel powerup;
    private void Awake()
    {
        // On vérifie qu'il n'y a pas d'autre instance du singleton
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
