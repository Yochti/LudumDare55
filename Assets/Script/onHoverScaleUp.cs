using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnHoverScaleUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject obj;
    private Vector3 originalScale;
    public float scaleFactor = 1.25f;
    public AudioSource audioSource;
    public Image image;
    private Color initialColor;
    public Color hoverColor;
    private void Start()
    {
        initialColor = image.color;
        originalScale = obj.transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
        if (!GunScriptShop.hasClicked)
        {
            obj.transform.localScale *= scaleFactor;
            if(audioSource!=null)audioSource.Play();
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = initialColor;
        if(!GunScriptShop.hasClicked)hoverExit();
    }
    public void hoverExit()
    {
        obj.transform.localScale = originalScale;
    }
}
