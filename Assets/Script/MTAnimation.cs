using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTAnimation : MonoBehaviour
{
    public Animator titleAnimation;
    private bool hasTouchedOnce;
    public GameObject buttonPack;
    public GameObject textContinue;
    public AudioSource audioo;
    public GameObject panelGameMode;
    public GameObject optionPanel;
    private void Update()
    {
        if(Input.anyKeyDown && !hasTouchedOnce)
        {
            titleAnimation.SetBool("hasPressed", true);
            audioo.Play();
            titleAnimation.SetBool("hasPressedReverse", false);


            hasTouchedOnce = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && hasTouchedOnce && !panelGameMode.activeSelf && !optionPanel.activeSelf)
        {
            titleAnimation.SetBool("hasPressed", false);
            titleAnimation.SetBool("hasPressedReverse", true);

            hasTouchedOnce = false;

        }
    }

    public void showButton()
    {
        buttonPack.SetActive(true);

    }
    public void showContinue()
    {
        textContinue.SetActive(true);

    }
    public void hideButton()
    {
        buttonPack.SetActive(false);

    }
    public void hideContinue()
    {
        textContinue.SetActive(false);

    }
}
