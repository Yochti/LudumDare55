using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class difficultyManager : MonoBehaviour
{
    public saveSytem save;
    public float percentageBonus = 100;
    public TextMeshProUGUI percentageText;
    public GameObject invocBlock;
    // Bools locaux pour suivi d'état
    bool generalBool;
    bool enemyAmout;
    bool xpAmount;
    bool reroll;
    bool noDash;
    bool noChest;
    bool noInvoc;
    bool noDrop;
    private void Start()
    {
        // Initialisation des bools à partir du save
        generalBool = save.generalDifficulty;
        enemyAmout = save.enemyAmountDifficulty;
        xpAmount = save.xpAmountDifficulty;
        reroll = save.rerollDifficulty;
        noDash = save.noDashDifficulty;
        noChest = save.noChestDifficulty;
        noInvoc = save.noInvocDifficulty;
        // Réinitialisation du bonus
        percentageBonus = 100f;
        invocBlock.SetActive(save.noInvocDifficulty);
        // On applique les bonus déjà activés
        if (generalBool) percentageBonus *= 1.15f;
        if (enemyAmout) percentageBonus *= 1.2f;
        if (xpAmount) percentageBonus *= 1.15f;
        if (reroll) percentageBonus *= 1.05f;
        if (noDash) percentageBonus *= 1.3f;
        if (noChest) percentageBonus *= 1.1f;
        if (noInvoc) percentageBonus *= 1.4f;
        if (noDrop) percentageBonus *= 1.05f;
        percentageBonus = Mathf.RoundToInt(percentageBonus);
        save.percentageBonus = percentageBonus;
        percentageText.text = "X" + percentageBonus.ToString() + "%";

        save.SaveData();
    }

    public void updateXpBonus(float bonus, bool isActivated)
    {
        if (isActivated)
            percentageBonus *= bonus;
        else
            percentageBonus /= bonus;

        percentageBonus = Mathf.RoundToInt(percentageBonus);
        save.percentageBonus = percentageBonus;
        percentageText.text = "X" + percentageBonus.ToString() + "%";

        save.SaveData();
    }

    public void generalDifficulty()
    {
        generalBool = !generalBool;
        save.generalDifficulty = generalBool;
        updateXpBonus(1.15f, generalBool);
    }

    public void enemyAmountDifficulty()
    {
        enemyAmout = !enemyAmout;
        save.enemyAmountDifficulty = enemyAmout;
        updateXpBonus(1.2f, enemyAmout);
    }

    public void xpAmountDifficulty()
    {
        xpAmount = !xpAmount;
        save.xpAmountDifficulty = xpAmount;
        updateXpBonus(1.15f, xpAmount);
    }

    public void rerollDifficulty()
    {
        reroll = !reroll;
        save.rerollDifficulty = reroll;
        updateXpBonus(1.05f, reroll);
    }

    public void noDashDifficulty()
    {
        noDash = !noDash;
        save.noDashDifficulty = noDash;
        updateXpBonus(1.3f, noDash);
    }

    public void noChestDifficulty()
    {
        noChest = !noChest;
        save.noChestDifficulty = noChest;
        updateXpBonus(1.1f, noChest);
    }
    public void NoDrop()
    {
        noDrop = !noDrop;
        save.noDrop = noDrop;
        updateXpBonus(1.1f, noDrop);
    }
    public void noInvocDifficulty()
    {
        noInvoc = !noInvoc;
        save.noInvocDifficulty = noInvoc;
        if(noInvoc)
        {
            save.InvocSlot1 = "";
            save.InvocSlot2 = "";
            save.InvocSlot3 = "";
            save.InvocSlot4 = "";
        }  
        invocBlock.SetActive(!invocBlock.activeSelf);
        updateXpBonus(1.4f, noInvoc);

    }
}

