using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtinctionManager : MonoBehaviour
{
    static int extinction_range = 4;
    static Animator animator;
    static GameObject canvas_child;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        canvas_child = transform.GetChild(0).gameObject;
        canvas_child.SetActive(false);
    }

    static int random_extinction = 0;

    public static void Display_Extinction_Information()
    {
        print("display 1");
        random_extinction = Random.Range(1, extinction_range);
        print("display 2");
        canvas_child.SetActive(true);
        print("display 3");
        animator.SetInteger("ExtinctionManager", random_extinction);
        print("display 4");
        PlayerInput.Add_To_Player_Input(MainMenu_StartFirstMiniGame);
    }

    public static void MainMenu_StartFirstMiniGame(Touch touch)
    {
        print("display 5");
        print("display 5.5: " + touch.phase);
        if (touch.phase != TouchPhase.Ended)
        {
            print("display 6");

            GlobalMinigameManager.Start_MiniGame();

            animator.SetInteger("ExtinctionManager", 0);
            canvas_child.SetActive(false);

            PlayerInput.Remove_From_Player_Input(MainMenu_StartFirstMiniGame);
        }
    }

    public static void Display_Final_Extinction_Information(CreatureAI current_creature_data)
    {
        animator.SetInteger("ExtinctionManager", random_extinction);

        PlayerInput.Add_To_Player_Input(Reset_Game_After_Final_Extinction);
    }

    public static void Reset_Game_After_Final_Extinction(Touch touch)
    {
        if (touch.phase != TouchPhase.Began) return;

        PlayerInput.Reset_Input();
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
