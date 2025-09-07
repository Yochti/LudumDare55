using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpecialBuyAndPlay : MonoBehaviour
{
    public saveSytem save;

    public Button specialButton;
    public Image progressBar;
    public Image background;

    public string specialName;
    public int price;

    public Color baseColor;
    public Color purchasedBarColor;
    public Color notOwnedColor;
    public Color ownedColor;
    public Color equippedColor;

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
        if (IsSpecialOwned())
        {
            save.whichSpecial = specialName;
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
        PurchaseSpecial();
        isHolding = false;
    }

    private void ResetProgressBar()
    {
        progressBar.fillAmount = 0f;
    }

    private void PurchaseSpecial()
    {
        if (save.totalSouls >= price)
        {
            save.totalSouls -= price;
            AssignSpecialOwnership();
            save.whichSpecial = specialName;
            progressBar.color = purchasedBarColor;
            save.SaveData();
            audioPurchase?.Play();
            UpdateVisualStateAll();
        }
        else
        {
            audioDenied?.Play();
            progressBar.fillAmount = 0f;
            background.color = notOwnedColor;
        }
    }

    private void UpdateVisualState()
    {
        if (!IsSpecialOwned())
        {
            background.color = notOwnedColor;
        }
        else if (save.whichSpecial == specialName)
        {
            background.color = equippedColor;
        }
        else
        {
            background.color = ownedColor;
        }

        specialButton.GetComponent<Image>().color = (save.whichSpecial == specialName) ? Color.green : baseColor;
    }

    private bool IsSpecialOwned()
    {
        return specialName == "Special1" ||
               (specialName == "Special2" && save.hasSpecial2) ||
               (specialName == "Special3" && save.hasSpecial3);
               
    }

    private void AssignSpecialOwnership()
    {
        switch (specialName)
        {
            case "Special2": save.hasSpecial2 = true; break;
            case "Special3": save.hasSpecial3 = true; break;
        }
    }

    private void UpdateVisualStateAll()
    {
        foreach (SpecialBuyAndPlay special in FindObjectsOfType<SpecialBuyAndPlay>())
        {
            special.UpdateVisualState();
        }
    }
}
