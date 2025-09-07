using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class difficultyUnlock : MonoBehaviour
{
    public saveSytem save;
    public int unlockLvl;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;
    public Button button;
    public GameObject buttonGO;
    public string lockedText;
    public string unlockedText;
    public DifficultyTooltip tooltip;
    private void Start()
    {
        UpdateUnlockState();
    }

    public void UpdateUnlockState()
    {
        bool unlocked = save.lvl >= unlockLvl;
        button.interactable = unlocked;
        if (unlocked) tooltip.tooltipMessage = unlockedText;
        else tooltip.tooltipMessage = lockedText;
        Transform imageChild = buttonGO.transform.Find("Image");
        if (imageChild != null)
        {
            Image img = imageChild.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = unlocked ? unlockedSprite : lockedSprite;
            }
            else
            {
                Debug.LogWarning("L'enfant 'Image' n'a pas de composant Image.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun enfant nommé 'Image' trouvé dans le bouton.");
        }
    }
}
