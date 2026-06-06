using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    static Animator animator;

    public void OnEnable()
    {
        current_background_type = 0;
        animator = GetComponent<Animator>();
        animator.SetInteger("BackgroundType", current_background_type);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_background_type = 0;

        animator = GetComponent<Animator>();
        animator.SetInteger("BackgroundType", current_background_type);
    }

    static int current_background_type = 0;

    public static void Increase_Background_Type()
    {
        current_background_type++;
        animator.SetInteger("BackgroundType", current_background_type);
    }
}
