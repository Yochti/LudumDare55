using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class XpManagerDeath : MonoBehaviour
{
    public TextMeshProUGUI textXP;
    public TextMeshProUGUI textLvl;
    public saveSytem save;
    public Slider slider;

    public void AddXP(float xp)
    {
        if (save == null)
        {
            Debug.LogError("SaveSystem non assigné !");
            return;
        }

        if (save.lvl <= 0) save.lvl = 1;

        Debug.Log("AddXP appelé. XP gagné : " + xp);
        StartCoroutine(AnimateXPAddition(xp));
    }

    private IEnumerator AnimateXPAddition(float xpToAdd)
    {
        float currentXP = save.xp;
        int currentLvl = save.lvl;

        while (xpToAdd > 0f)
        {
            float maxXpThisLevel = currentLvl * 100f;
            float xpNeeded = maxXpThisLevel - currentXP;

            float xpStep = Mathf.Min(xpToAdd, xpNeeded);
            float startXP = currentXP;
            float endXP = currentXP + xpStep;
            float duration = 0.5f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime; 
                float interpolatedXP = Mathf.Lerp(startXP, endXP, elapsed / duration);
                slider.value = interpolatedXP / maxXpThisLevel;
                textXP.text = Mathf.FloorToInt(interpolatedXP) + "/" + maxXpThisLevel;
                textLvl.text = "LV" + currentLvl;
                yield return null;
            }

            currentXP = endXP;
            xpToAdd -= xpStep;

            if (currentXP >= maxXpThisLevel)
            {
                currentLvl++;
                if (currentLvl % 5 == 0) save.totalSouls += 3;
                else {
                    save.totalSouls+=1;
                } 
                currentXP = 0f;
            }
        }

        save.xp = currentXP;
        save.lvl = currentLvl;
        save.SaveData();

        float maxXPFinal = currentLvl * 100f;
        slider.value = currentXP / maxXPFinal;
        textXP.text = Mathf.FloorToInt(currentXP) + "/" + maxXPFinal;
        textLvl.text = "LV" + currentLvl;

        Debug.Log("XP final : " + currentXP + " | LVL : " + currentLvl);
    }
}
