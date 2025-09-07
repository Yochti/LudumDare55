using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipInvoc : MonoBehaviour
{
    public saveSytem save;
    public Image backgroundColor;
    public Color equippedColor;
    public Color baseColor;
    public string invocName;

    private void Update()
    {
        bool isEquipped = save.InvocSlot1 == invocName ||
                          save.InvocSlot2 == invocName ||
                          save.InvocSlot3 == invocName ||
                          save.InvocSlot4 == invocName;

        backgroundColor.color = isEquipped ? equippedColor : baseColor;
    }

    public void EquipInvocToSlot()
    {
        if (!IsInvocOwned(invocName)) return;

        
        if (save.InvocSlot1 == invocName) save.InvocSlot1 = "";
        else if (save.InvocSlot2 == invocName) save.InvocSlot2 = "";
        else if (save.InvocSlot3 == invocName) save.InvocSlot3 = "";
        else if (save.InvocSlot4 == invocName) save.InvocSlot4 = "";
        else
        {
            if (!string.IsNullOrEmpty(save.InvocSlot1) &&
                !string.IsNullOrEmpty(save.InvocSlot2) &&
                !string.IsNullOrEmpty(save.InvocSlot3) &&
                !string.IsNullOrEmpty(save.InvocSlot4))
            {
                return;
            }

            if (string.IsNullOrEmpty(save.InvocSlot1)) save.InvocSlot1 = invocName;
            else if (string.IsNullOrEmpty(save.InvocSlot2)) save.InvocSlot2 = invocName;
            else if (string.IsNullOrEmpty(save.InvocSlot3)) save.InvocSlot3 = invocName;
            else if (string.IsNullOrEmpty(save.InvocSlot4)) save.InvocSlot4 = invocName;
        }

        save.SaveData();
    }

    private bool IsInvocOwned(string name)
    {
        return name == "Bababoy" ||
               name == "Name" ||
               name == "Junktrap" ||
               name == "Hermes" ||
               (name == "Dimitri" && save.hasInvoc5) ||
               (name == "Hera" && save.hasInvoc6) ||
               (name == "Izo" && save.hasInvoc7);
    }

}
