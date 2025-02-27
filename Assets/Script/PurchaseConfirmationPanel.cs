using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PurchaseConfirmationPanel : MonoBehaviour
{
    public TextMeshProUGUI confirmationText;           // Texte pour le prix et nom de l'arme
    public Button confirmButton;            // Bouton pour confirmer l'achat
    public Button cancelButton;             // Bouton pour annuler
    private GunBuyAndPlay gunBuyAndPlay;    // Référence au script GunBuyAndPlay

    private string selectedGun;
    private int selectedGunPrice;

    private void Start()
    {
        // Masque le panel de confirmation au début
        gameObject.SetActive(false);
        confirmButton.onClick.AddListener(ConfirmPurchase);
        cancelButton.onClick.AddListener(CancelPurchase);
    }

    public void ShowConfirmation(string gunName, int price, GunBuyAndPlay gunBuyAndPlayRef)
    {
        selectedGun = gunName;
        selectedGunPrice = price;
        gunBuyAndPlay = gunBuyAndPlayRef;
        confirmationText.text = $"Acheter {gunName} pour {price} âmes ?";
        gameObject.SetActive(true);
    }

    public void ConfirmPurchase()
    {
        gunBuyAndPlay.PurchaseWeapon(selectedGun, selectedGunPrice);
        gameObject.SetActive(false);
    }

    public void CancelPurchase()
    {
        gameObject.SetActive(false);
    }
}
