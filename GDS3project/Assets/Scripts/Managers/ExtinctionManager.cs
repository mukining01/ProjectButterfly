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
        random_extinction = Random.Range(1, extinction_range);

        canvas_child.SetActive(true);
        animator.SetInteger("ExtinctionManager", random_extinction);

        PlayerInput.Add_To_Player_Input(MainMenu_StartFirstMiniGame);
    }

    public static void MainMenu_StartFirstMiniGame(Touch touch)
    {
        if (touch.phase != TouchPhase.Began) return;

        GlobalMinigameManager.Start_MiniGame();

        animator.SetInteger("ExtinctionManager", 0);
        canvas_child.SetActive(false);

        PlayerInput.Remove_From_Player_Input(MainMenu_StartFirstMiniGame);
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
