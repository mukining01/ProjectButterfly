using TMPro;
using UnityEngine;

public class TimeAnimator : MonoBehaviour
{
    public TMP_Text time_text_p;
    public TMP_Text time_under_text_p;

    static TMP_Text time_text;
    static TMP_Text time_under_text;
    public float delay_between_time = 0.5f;
    static float current_delay_between_time = 0;
    public float time_it_takes_to_reach_new_time = 1;
    static float current_time_it_takes_to_reach_new_time = 0;
    static int[] time_intervals = new int[] {407, 250, 1};
    static float current_time = 541;
    static float new_time = 0;
    static float old_time = 0;
    static int increment = 0;
    static bool played_sfx;

    private void Start()
    {
        time_text = time_text_p;
        time_under_text = time_under_text_p;
    }

    public static void Increase_Time()
    {
        current_delay_between_time = 0;
        current_time_it_takes_to_reach_new_time = 0;

        old_time = current_time;
        new_time = time_intervals[increment];
        increment++;

        played_sfx = false;
    }

    public void Update()
    {
        time_text.text = current_time.ToString();
        time_under_text.text = current_time.ToString();

        if (current_time == new_time) return;

        if(current_delay_between_time < delay_between_time)
        {
            current_delay_between_time += Time.deltaTime;
            return;
        }

        current_time_it_takes_to_reach_new_time += Time.deltaTime;
        current_time = Mathf.Lerp(old_time, new_time, current_time_it_takes_to_reach_new_time / time_it_takes_to_reach_new_time);
        current_time = Mathf.Round(current_time);
        if (current_time_it_takes_to_reach_new_time >= time_it_takes_to_reach_new_time)
        {
            current_time = new_time;
        }
    }
}
