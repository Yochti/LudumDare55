using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunBuyAndPlay : MonoBehaviour
{
    public saveSytem save;

    public Button gunButton;
    public Image progressBar;
    public Image background;

    public string gunName;
    public int price;

    public Color baseColor;           // Couleur du bouton par défaut
    public Color purchasedBarColor;   // Couleur de la barre après achat
    public Color notOwnedColor;       // Couleur du fond quand pas encore acheté
    public Color ownedColor;          // Couleur du fond quand acheté
    public Color equippedColor;       // Couleur du fond quand équipé

    public AudioSource audioPurchase;
    public AudioSource audioDenied;

    private bool isHolding = false;
    private float holdTime = 1f;
    private Coroutine holdCoroutine;


    private void Update()
    {
         UpdateVisualState();
    }

    public void OnPointerDown()
    {
        if (IsWeaponOwned())
        {
            save.whichWeapon = gunName;
            save.SaveData();
            UpdateVisualStateAll(); 
        }
        else if (!isHolding)
        {
            holdCoroutine = StartCoroutine(HoldToBuy());
        }
    }

    public void OnPointerUp()
    {
        if (isHolding && holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            ResetProgressBar();
            isHolding = false;
        }
    }

    private IEnumerator HoldToBuy()
    {
        isHolding = true;
        float elapsedTime = 0f;
        progressBar.fillAmount = 0f;

        while (elapsedTime < holdTime)
        {
            elapsedTime += Time.deltaTime;
            progressBar.fillAmount = elapsedTime / holdTime;
            yield return null;
        }

        progressBar.fillAmount = 1f;
        PurchaseWeapon();
        isHolding = false;
    }

    private void ResetProgressBar()
    {
        progressBar.fillAmount = 0f;
    }

    private void PurchaseWeapon()
    {
        if (save.totalSouls >= price)
        {
            save.totalSouls -= price;
            AssignWeaponOwnership();
            save.whichWeapon = gunName;
            progressBar.color = purchasedBarColor;
            save.SaveData();
            audioPurchase?.Play();
            UpdateVisualStateAll(); // rafraîchir tout
        }
        else
        {
            audioDenied?.Play();
            progressBar.fillAmount = 0f;
            background.color = notOwnedColor;
            return;
        }
    }

    private void UpdateVisualState()
    {
        if (!IsWeaponOwned())
        {
            background.color = notOwnedColor;
        }
        else if (save.whichWeapon == gunName)
        {
            background.color = equippedColor;
        }
        else
        {
            background.color = ownedColor;
        }

        // Couleur du bouton sélectionné
        gunButton.GetComponent<Image>().color = (save.whichWeapon == gunName) ? Color.green : baseColor;
    }

    private bool IsWeaponOwned()
    {
        return gunName == "Gun1" ||
               (gunName == "Gun3" && save.hasGun3) ||
               (gunName == "Gun4" && save.hasGun4) ||
               (gunName == "Gun6" && save.hasGun6) ||
               (gunName == "Gun7" && save.hasGun7);
    }

    private void AssignWeaponOwnership()
    {
        switch (gunName)
        {
            case "Gun3": save.hasGun3 = true; break;
            case "Gun4": save.hasGun4 = true; break;
            case "Gun6": save.hasGun6 = true; break;
            case "Gun7": save.hasGun7 = true; break;
        }
    }

    private void UpdateVisualStateAll()
    {
        foreach (GunBuyAndPlay gun in FindObjectsOfType<GunBuyAndPlay>())
        {
            gun.UpdateVisualState();
        }
    }
}
