using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class InvocShopBuy : MonoBehaviour
{
    public saveSytem save;

    public Button invocButton;
    public Image progressBar;
    public Image background;
    public Image background2;
    public TextMeshProUGUI selectionText;

    public string invocName;
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

    private void Start()
    {
        LoadSelectedInvocations();
        UpdateVisualState();
    }

    private void Update()
    {
        UpdateVisualState();
    }

    public void OnPointerDown()
    {
        if (IsInvocationOwned())
        {
            ToggleInvocation();
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
        PurchaseInvocation();
        isHolding = false;
    }

    private void PurchaseInvocation()
    {
        if (save.totalSouls >= price)
        {
            save.totalSouls -= price;
            AssignInvocationOwnership();
            audioPurchase?.Play();
            save.SaveData();
            ResetProgressBar();
            UpdateVisualStateAll();
        }
        else
        {
            audioDenied?.Play();
            ResetProgressBar();
        }
    }

    private void ResetProgressBar()
    {
        progressBar.fillAmount = 0f;
    }

    private void ToggleInvocation()
    {
        List<string> selected = GetSelectedInvocations();

        if (selected.Contains(invocName))
        {
            selected.Remove(invocName);
        }
        else if (selected.Count < 4)
        {
            selected.Add(invocName);
        }

        SaveSelectedInvocations(selected);
        save.SaveData();
        UpdateVisualStateAll();
    }

    private void UpdateVisualState()
    {
        List<string> selected = GetSelectedInvocations();

        if (!IsInvocationOwned())
        {
            background2.color = notOwnedColor;
            background.color = notOwnedColor;
            selectionText.text = "";
        }
        else if (selected.Contains(invocName))
        {
            background2.color = equippedColor;
            background.color = equippedColor;
            selectionText.text = (selected.IndexOf(invocName) + 1).ToString();
        }
        else
        {
            background2.color = ownedColor;
            background.color = ownedColor;
            selectionText.text = "";
        }

    }

    private void UpdateVisualStateAll()
    {
        foreach (InvocShopBuy invoc in FindObjectsOfType<InvocShopBuy>())
        {
            invoc.UpdateVisualState();
        }
    }

    private bool IsInvocationOwned()
    {
        return invocName == "Bababoy" ||
               invocName == "Name" ||
               invocName == "Junktrap" ||
               invocName == "Hermes" ||
               (invocName == "Dimitri" && save.hasInvoc5) ||
               (invocName == "Hera" && save.hasInvoc6) ||
               (invocName == "Izo" && save.hasInvoc7); 
    }

    private void AssignInvocationOwnership()
    {
        switch (invocName)
        {
            case "Dimitri": save.hasInvoc5 = true; break;
            case "Hera": save.hasInvoc6 = true; break;
            case "Izo": save.hasInvoc7 = true; break; 
        }
    }


    private List<string> GetSelectedInvocations()
    {
        List<string> selected = new List<string>();
        if (!string.IsNullOrEmpty(save.InvocSlot1)) selected.Add(save.InvocSlot1);
        if (!string.IsNullOrEmpty(save.InvocSlot2)) selected.Add(save.InvocSlot2);
        if (!string.IsNullOrEmpty(save.InvocSlot3)) selected.Add(save.InvocSlot3);
        if (!string.IsNullOrEmpty(save.InvocSlot4)) selected.Add(save.InvocSlot4);
        return selected;
    }

    private void SaveSelectedInvocations(List<string> selected)
    {
        save.InvocSlot1 = selected.Count > 0 ? selected[0] : "";
        save.InvocSlot2 = selected.Count > 1 ? selected[1] : "";
        save.InvocSlot3 = selected.Count > 2 ? selected[2] : "";
        save.InvocSlot4 = selected.Count > 3 ? selected[3] : "";
    }

    private void LoadSelectedInvocations()
    {
        // Cette fonction est gardée au cas où tu veux initialiser manuellement plus tard
    }
}
