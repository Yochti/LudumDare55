using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTAnimation : MonoBehaviour
{
    public Animator titleAnimation;
    private bool hasTouchedOnce;
    public GameObject buttonPack;
    public GameObject textContinue;
    private void Update()
    {
        if(Input.anyKeyDown && !hasTouchedOnce)
        {
            titleAnimation.SetBool("hasPressed", true);
            titleAnimation.SetBool("hasPressedReverse", false);


            hasTouchedOnce = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && hasTouchedOnce)
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
