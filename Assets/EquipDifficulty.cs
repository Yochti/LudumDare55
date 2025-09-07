using UnityEngine;
using UnityEngine.UI;

public class EquipDifficulty : MonoBehaviour
{
    public enum DifficultyType { General, EnemyAmount, XpAmount, Reroll, NoDash, NoChest, NoInvoc, NoDrop }
    public DifficultyType difficultyType;

    public saveSytem save;

    public Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 normalScale = Vector3.one;

    public Color selectedColor = new Color(1f, 1f, 1f, 1f); 
    public Color normalColor = new Color(0.8f, 0.8f, 0.8f, 1f); 

    private RectTransform rectTransform;
    private Image background;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        background = transform.Find("Background").GetComponent<Image>();
        UpdateVisual();
    }

    void Update()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        bool isActive = false;

        switch (difficultyType)
        {
            case DifficultyType.General:
                isActive = save.generalDifficulty;
                break;
            case DifficultyType.EnemyAmount:
                isActive = save.enemyAmountDifficulty;
                break;
            case DifficultyType.XpAmount:
                isActive = save.xpAmountDifficulty;
                break;
            case DifficultyType.Reroll:
                isActive = save.rerollDifficulty;
                break;
            case DifficultyType.NoDash:
                isActive = save.noDashDifficulty;
                break;
            case DifficultyType.NoChest:
                isActive = save.noChestDifficulty;
                break;
            case DifficultyType.NoInvoc:
                isActive = save.noInvocDifficulty;
                break;
            case DifficultyType.NoDrop:
                isActive = save.noDrop;
                break;
        }

        rectTransform.localScale = isActive ? selectedScale : normalScale;
        if (background != null)
            background.color = isActive ? selectedColor : normalColor;
    }
}
