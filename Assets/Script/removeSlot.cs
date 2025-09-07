using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeSlot : MonoBehaviour
{
    public saveSytem save;

    public void removeSlot1()
    {
        save.InvocSlot1 = "";
    }
    public void removeSlot2()
    {
        save.InvocSlot2 = "";
    }
    public void removeSlot3()
    {
        save.InvocSlot3 = "";
    }
    public void removeSlot4()
    {
        save.InvocSlot4 = "";
    }
}
