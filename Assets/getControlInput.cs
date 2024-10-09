using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class getControlInput : MonoBehaviour
{
    //Input References
    public TMP_InputField input1;
    public TMP_InputField input2;
    public TMP_InputField input3;
    public TMP_InputField input4;
    public TMP_InputField input5;

    public string s1;
    public string s2;
    public string s3;
    public string s4;
    public string s5;

    public GameObject panelInfo;

    //Save References
    public saveSytem save;
    private void Awake()
    {
        save.LoadData();
        s1 = input1.text;
        s2 = input2.text;
        s3 = input3.text;
        s4 = input4.text;
        s5 = input5.text;

        if (save.UpKey == null)
            input1.text = "W";
        else
            input1.text = save.UpKey;

        if (save.DownKey == null)
            input2.text = "S";
        else
            input2.text = save.DownKey;

        if (save.LeftKey == null)
            input3.text = "A";
        else
            input3.text = save.LeftKey;
        if (save.RightKey == null)
            input4.text = "D";
        else
            input4.text = save.RightKey;

        if (save.UpKey == null)
            input5.text = "Space";
        else
            input5.text = save.DashKey;
    }
    private void Update()
    {
        if (string.IsNullOrWhiteSpace(input5.text))
            input5.text = "Space";
        
        save.UpKey = input1.text.ToUpper();
        save.DownKey = input2.text.ToUpper();
        save.LeftKey = input3.text.ToUpper();
        save.RightKey= input4.text.ToUpper();
        save.DashKey = input5.text;
        if (input1.text != s1 || input2.text != s2 || input3.text != s3 || input4.text != s4 || input5.text != s5)
        {
            save.SaveData();
            s1 = input1.text;
            s2 = input2.text;
            s3 = input3.text;
            s4 = input4.text;
            s5 = input5.text;
        }
    }
    public void openAndClosePanel()
    {
        bool isActive = panelInfo.activeSelf;
        panelInfo.SetActive(!isActive);
    }

}
