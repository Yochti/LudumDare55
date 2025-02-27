using UnityEngine;
using UnityEngine.UI;

public class GunBuyAndPlay : MonoBehaviour
{
    public saveSytem save;
    public Button gun1sprite, gun2sprite, gun3sprite, gun4sprite, gun5sprite, gun6sprite, gun7sprite, gun8sprite;
    public Color baseColor;
    public AudioSource audioo;public AudioSource audioo2;
    public PurchaseConfirmationPanel confirmationPanel;  // Référence au panneau de confirmation

    private void Update()
    {
        UpdateGunColors();
    }

    private void UpdateGunColors()
    {
        Button[] buttons = { gun1sprite, gun2sprite, gun3sprite, gun4sprite, gun5sprite, gun6sprite, gun7sprite, gun8sprite };
        foreach (Button button in buttons) button.GetComponent<Image>().color = baseColor;

        // Applique la couleur verte à l'arme sélectionnée
        if (save.whichWeapon == "Gun1") gun1sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun2") gun2sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun3") gun3sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun4") gun4sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun5") gun5sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun6") gun6sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun7") gun7sprite.GetComponent<Image>().color = Color.green;
        else if (save.whichWeapon == "Gun8") gun8sprite.GetComponent<Image>().color = Color.green;
    }

    public void PurchaseWeapon(string gunName, int price)
    {
        // Vérifie les âmes et effectue l'achat
        if (save.TotalSouls >= price)
        {
            audioo.Play();
            save.TotalSouls -= price;
            AssignWeaponOwnership(gunName);
            save.SaveData();
        }
        else audioo2.Play();
    }

    private void AssignWeaponOwnership(string gunName)
    {
        save.whichWeapon = gunName;
        switch (gunName)
        {
            case "Gun2": save.hasGun2 = true; break;
            case "Gun3": save.hasGun3 = true; break;
            case "Gun4": save.hasGun4 = true; break;
            case "Gun5": save.hasGun5 = true; break;
            case "Gun6": save.hasGun6 = true; break;
            case "Gun7": save.hasGun7 = true; break;
            case "Gun8": save.hasGun8 = true; break;
        }
    }

    // Méthodes pour chaque arme
    public void Gun1() { save.whichWeapon = "Gun1"; save.SaveData(); }
    public void Gun2() { TryPurchaseWeapon("Gun2", 250); }
    public void Gun3() { TryPurchaseWeapon("Gun3", 50); }
    public void Gun4() { TryPurchaseWeapon("Gun4", 500); }
    public void Gun5() { TryPurchaseWeapon("Gun5", 1500); }
    public void Gun6() { TryPurchaseWeapon("Gun6", 2000); }
    public void Gun7() { TryPurchaseWeapon("Gun7", 5000); }
    public void Gun8() { TryPurchaseWeapon("Gun8", 5000); }

    private void TryPurchaseWeapon(string gunName, int price)
    {
        if (HasWeapon(gunName))
        {
            save.whichWeapon = gunName;
            save.SaveData();
        }
        else
        {
            confirmationPanel.ShowConfirmation(gunName, price, this);
        }
    }

    private bool HasWeapon(string gunName)
    {
        switch (gunName)
        {
            case "Gun2": return save.hasGun2;
            case "Gun3": return save.hasGun3;
            case "Gun4": return save.hasGun4;
            case "Gun5": return save.hasGun5;
            case "Gun6": return save.hasGun6;
            case "Gun7": return save.hasGun7;
            case "Gun8": return save.hasGun8;
            default: return false;
        }
    }
}
