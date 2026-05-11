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
        //PlayerInput.Add_To_Player_Input(MainMenuInput);
    }

    public static void MainMenuInput(Touch touch)
    {
        print("main menu");

        if (touch.phase == TouchPhase.Began)
        {

            MainMenu_Input();
        }
    }

    public static void MainMenu_Input()
    {
        //print("main menu 1");
        animator.SetTrigger("clicked");
        //print("main menu 2");
        PlayerInput.Remove_From_Player_Input(MainMenuInput);
       // print("main menu 3");

        RestartManager.Decreasing_Restart_time();

        ExtinctionManager.Display_First_Extinction_Information();
        print("main menu 4");

    }

}
