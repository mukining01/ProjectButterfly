using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtinctionManager : MonoBehaviour
{
    static int extinction_range = 4;
    static Animator animator;
    public GameObject extinction_image_p;
    static GameObject extinction_image;

    static float ExtinctionMultiplier = 100000;
    static float new_ExtinctionTime = 0;
    static float current_ExtinctionTime = 0;
    static float old_ExtinctionTime = 0;

    static float extinction_time_increase_lerp_max_time = 2;
    static float extinction_time_increase_lerp_current_time = 0;

    public string[] Extinction_Types;
    public TMP_Text extinction_type;
    public TMP_Text extinction_time;
    public Animator Show_Text_animator_p;
    static Animator Show_Text_animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        extinction_image = extinction_image_p;
        extinction_image.SetActive(false);

        Show_Text_animator = Show_Text_animator_p;

    }

    private void Start()
    {

        Set_Extinction_Time();
    }

    static int random_extinction = 0;

    public static void Display_Extinction_Information()
    {
        extinction_image.SetActive(true);
        print("display 3");
        animator.SetInteger("ExtinctionManager", random_extinction);
        print("display 4");
        PlayerInput.Add_To_Player_Input(Extinction_Input);

        Show_Text_animator.SetBool("Show", true);
    }

    public static void Extinction_Input(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            if (GlobalMinigameManager.Last_Minigame()) Display_Final_Extinction_Information();
            else Continue_Minigame();
        }
    }

    public static void Continue_Minigame()
    {
        GlobalMinigameManager.Start_MiniGame();

        extinction_image.SetActive(false);

        Show_Text_animator.SetBool("Show", false);

        PlayerInput.Remove_From_Player_Input(Extinction_Input); 
    }

    public static void Display_Final_Extinction_Information()
    {
        animator.SetInteger("ExtinctionManager", random_extinction);

        PlayerInput.Add_To_Player_Input(Reset_Game_After_Final_Extinction);
    }

    public static void Reset_Game_After_Final_Extinction(Touch touch)
    {
        if (touch.phase == TouchPhase.Began) RestartManager.Restart();


    }

    public void Set_Extinction_Time()
    {
        random_extinction = Random.Range(1, extinction_range);

        extinction_type.text = Extinction_Types[random_extinction -  1];
        current_ExtinctionTime = ExtinctionMultiplier * GlobalMinigameManager.Get_Minigame_Amount();
        new_ExtinctionTime = current_ExtinctionTime;
    }

    public static void Set_New_Extinction_Time()
    {
        new_ExtinctionTime -= ExtinctionMultiplier;
        old_ExtinctionTime = current_ExtinctionTime;

        extinction_time_increase_lerp_current_time = 0;
    }

    public void FixedUpdate()
    {

        print("time time: ");
        extinction_time.text = current_ExtinctionTime.ToString() + " years";
        Lerp_Extinction_Time();
    }

    public void Lerp_Extinction_Time()
    {
        if (current_ExtinctionTime != new_ExtinctionTime)
        {
            extinction_time_increase_lerp_current_time += Time.deltaTime;
            if(extinction_time_increase_lerp_current_time >= extinction_time_increase_lerp_max_time)
            {
                current_ExtinctionTime = new_ExtinctionTime;
                return;
            }

            current_ExtinctionTime = Mathf.Lerp(old_ExtinctionTime, new_ExtinctionTime, extinction_time_increase_lerp_current_time / extinction_time_increase_lerp_max_time);
            current_ExtinctionTime = Mathf.Round(current_ExtinctionTime);
        }
    }

    public void Reduce_Extinction_Time()
    {

    }
}
