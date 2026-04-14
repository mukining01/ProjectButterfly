using UnityEngine;

public class EatenBarElement : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Set_EatenType(int eaten_type)
    {
        animator.SetInteger("EatenType", eaten_type);
    }
}
