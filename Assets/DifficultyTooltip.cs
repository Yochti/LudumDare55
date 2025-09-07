using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipMessage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipManager.Instance.ShowTooltip(tooltipMessage, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipManager.Instance.HideTooltip();
    }
}
