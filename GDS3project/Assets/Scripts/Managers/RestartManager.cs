using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    static float max_restart_time = 60;
    static float current_restart_time = 0;

    static bool decrease_restart_time = false;

    public static void Set_Restart_Time()
    {
        current_restart_time = max_restart_time;
    }

    public static void Decreasing_Restart_time()
    {
        decrease_restart_time = true;
    }

    public void Awake()
    {
        Set_Restart_Time();
    }

    public void OnEnable()
    {
        PlayerInput.Add_To_Player_Input(InputDetected);
    }

    public void OnDisable()
    {
        PlayerInput.Remove_From_Player_Input(InputDetected);
    }

    public static void InputDetected(Touch touch)
    {
        Set_Restart_Time();
    }

    public static void Restart(bool _ignore_pause = false)
    {
        if (!MainMenu.Is_Paused() && !_ignore_pause) return;

        PlayerInput.Reset_Input();
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Update()
    {


        if (!MainMenu.Game_Has_Started()) return;

        current_restart_time -= Time.deltaTime;

        if (current_restart_time <= 0) Restart(true);
    }

    public static void Force_Restart()
    {
        Restart(true);
    }
}
