using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    static Animator animator;

    static bool main_menu_on;

    public Animator pause_animator_p;
    static Animator pause_animator;

    public Animator global_restart_animator_p;
    static Animator global_restart_animator;

    static bool started = false;

    public void OnEnable()
    {
        paused = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Start_MainMenu();

        pause_animator = pause_animator_p;
        global_restart_animator = global_restart_animator_p;
    }

    private void Start()
    {
        AudioManager.Play_SFX(SFX.SS_BackgroundMusic);
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

        Time.timeScale = 1;

        AudioManager.Play_SFX(SFX.SS_StartButton);

        animator.SetTrigger("clicked");

        pause_animator.SetBool("Idle", true);
        global_restart_animator.SetBool("Restart", false);

        if (paused) return;

        AudioManager.Play_SFX(SFX.SS_TransitionUP);

        AudioManager.Play_SFX(SFX.SS_BackgroundMusic, false);
        started = true;

        //print("main menu 2");
        PlayerInput.Remove_From_Player_Input(MainMenuInput);
       // print("main menu 3");

        RestartManager.Decreasing_Restart_time();

        ExtinctionManager.Display_First_Extinction_Information();
        print("main menu 4");

    }

    static bool paused = false;

    public static void Paused()
    {
        paused = true;

        Time.timeScale = 0;

        animator.SetTrigger("paused");
        pause_animator.SetBool("Idle", false);
        global_restart_animator.SetBool("Restart", true);
    }

    public static bool Is_Paused()
    {
        return paused;
    }

    public static bool Game_Has_Started()
    {
        return started;
    }

}
