using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;
public class DamagePopUp : MonoBehaviour
{
    public static DamagePopUp Create(Vector3 position, int damageAmount, bool isCritical)
    {
        Transform damagePopUpTransform = Instantiate(GameAsset.i.pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCritical);
        return damagePopUp;
    }

    private const float DISAPPEAR_TIMER_MAX = 1f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }  
    public void Setup(int damageAmount, bool isCritical)
    {
        textMesh.SetText(damageAmount.ToString());
        
        if(!isCritical)
        {
            textMesh.fontSize = 4;
            textColor = UtilsClass.GetColorFromString("FF9104");
        }
        else
        {
            textMesh.fontSize = 6;
            textColor = UtilsClass.GetColorFromString("FF0904");

        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(1, 1) * 10f;

    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        if(disappearTimer> DISAPPEAR_TIMER_MAX *.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if(disappearTimer<=0)
        {
            float disaperringSpeed = 3f;
            textColor.a -= disaperringSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
