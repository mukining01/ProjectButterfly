using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtinctionManager : MonoBehaviour
{
    static int extinction_range = 2;

    static float ExtinctionMultiplier = 100000;
    static float new_ExtinctionTime = 0;
    static float current_ExtinctionTime = 0;
    static float old_ExtinctionTime = 0;

    static float extinction_time_increase_lerp_max_time = 2;
    static float extinction_time_increase_lerp_current_time = 0;

    public Animator anim_extinction_info_p;
    public Animator anim_extinction_text_p;
    public Animator anim_extinction_image_p;
    public Animator anim_transition_arrow_p;

    static Animator anim_extinction_info;
    static Animator anim_extinction_text;
    static Animator anim_extinction_image;
    static Animator anim_transition_arrow;

    public void Awake()
    {
        anim_extinction_text = anim_extinction_text_p;
        anim_extinction_image = anim_extinction_image_p;
        anim_transition_arrow = anim_transition_arrow_p;
        anim_extinction_info = anim_extinction_info_p;
    }

    private void Start()
    {
        Set_Extinction_Time();
    }

    static int random_extinction = 0;

    public static void Display_First_Extinction_Information()
    {
        random_extinction = Random.RandomRange(0, extinction_range);
        anim_extinction_text.SetInteger("Extinction", random_extinction);
        anim_extinction_image.SetInteger("Extinction", random_extinction);

        CameraManager.Switch_Camera(Camera_Types.ExtinctionCamera);

        timer_count = true;
    }

    public static void Display_Extinction_Information()
    {
        timer_count = true;
        current_arrow_pointer++;
        anim_extinction_info.SetBool("Active", true);
        //PlayerInput.Add_To_Player_Input(Extinction_Input);
    }

    static bool timer_count = false;
    static int current_arrow_pointer = 0;
    float max_time = 7;
    float current_time = 0;
    bool first_timer = false;

    public void Update()
    {
        if (!timer_count) return;

        current_time += Time.deltaTime;

        if(current_time >= max_time)
        {
            timer_count = false;
            first_timer = true;
            CameraManager.Switch_Camera(Camera_Types.MainCamera);
            Continue_Minigame();

            anim_extinction_info.SetBool("Active", false);
        } else if (current_time >= max_time / 2 && first_timer)
        {
            anim_transition_arrow.SetInteger("Arrow", current_arrow_pointer);
        }
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

        PlayerInput.Remove_From_Player_Input(Extinction_Input); 
    }

    public static void Display_Final_Extinction_Information()
    {
        PlayerInput.Add_To_Player_Input(Reset_Game_After_Final_Extinction);
    }

    public static void Reset_Game_After_Final_Extinction(Touch touch)
    {
        if (touch.phase == TouchPhase.Began) RestartManager.Restart();


    }

    public void Set_Extinction_Time()
    {
        random_extinction = Random.Range(1, extinction_range);

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

public enum Extinction_Events
{
    Ice_Age,
    Drought
}
