using UnityEngine;

public class ShrinkAndGrowSprite : MonoBehaviour
{
    Animator animator;

    public void Set_Size(int size)
    {
        if(animator == null) animator = GetComponent<Animator>();

        animator.SetInteger("Size", size);
    }
}
