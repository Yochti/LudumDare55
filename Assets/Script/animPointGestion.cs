using UnityEngine;

public class AnimPointGestion : MonoBehaviour
{
    public Animator anim;
    private string animationName = "UIAnim"; // Remplace par le nom r�el de l'animation

    public void playAnim()
    {
        if (anim != null)
        {
            anim.Play(animationName);
        }

    }
}
