using UnityEngine;

public class BossDropSoulCollect : MonoBehaviour
{
    private void OnDestroy()
    {
        PowerUp();
    }

    void PowerUp()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.ShowPowerPanel();
        }
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
}
