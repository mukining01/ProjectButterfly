using UnityEngine;

public class MainMenu : MonoBehaviour
{
    static Animator animator;

    static bool main_menu_on;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Start_MainMenu();
    }

    public static void Start_MainMenu()
    {
       // animator.SetTrigger("active");
        PlayerInput.Add_To_Player_Input(MainMenuInput);
    }

    public static void MainMenuInput(Touch touch)
    {
        print("main menu");

        if (touch.phase == TouchPhase.Began)
        {
            animator.SetTrigger("clicked");
            PlayerInput.Remove_From_Player_Input(MainMenuInput);

            ExtinctionManager.Display_Extinction_Information();
        }
    }


}
