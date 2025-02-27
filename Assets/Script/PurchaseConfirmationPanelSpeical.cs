using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseConfirmationPanelSpecial : MonoBehaviour
{
    public TextMeshProUGUI confirmationText;
    public Button confirmButton;
    public Button cancelButton;

    private Special specialScript;
    private string specialName;
    private int specialPrice;

    private void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        cancelButton.onClick.AddListener(HideConfirmation);
    }

    public void ShowConfirmation(string name, int price, Special special)
    {
        specialName = name;
        specialPrice = price;
        specialScript = special;
        confirmationText.text = $"Voulez-vous acheter {name} pour {price} âmes ?";
        gameObject.SetActive(true);
    }

    public void OnConfirm()
    {
        specialScript.PurchaseSpecial(specialName, specialPrice);
        HideConfirmation();
    }

    public void HideConfirmation()
    {
        gameObject.SetActive(false);
    }
}
