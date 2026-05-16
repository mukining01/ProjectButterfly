using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    static Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    static int current_background_type = 0;

    public static void Increase_Background_Type()
    {
        current_background_type++;
        animator.SetInteger("BackgroundType", current_background_type);
    }
}
