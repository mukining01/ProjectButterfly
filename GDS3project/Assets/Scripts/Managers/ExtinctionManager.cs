
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    public Animator anim_extinction_icon_p;
    public GameObject Continue_Button_p;


    static Animator anim_extinction_info;
    static Animator anim_extinction_text;
    static Animator anim_extinction_image;
    static Animator anim_transition_arrow;
    static Animator anim_extinction_icon;
    static GameObject Continue_Button;

    [Header("ExtinctionTextInformation")]
    public List<Extinction_Ending_Information> Extinction_Text_Info_p = new List<Extinction_Ending_Information>();
    static List<Extinction_Ending_Information> Extinction_Text_Info = new List<Extinction_Ending_Information>();
    public TMP_Text[] extinction_text_p;
    static TMP_Text[] extinction_text;
    static Extinction_Event extinction_event;


    public void Awake()
    {
        anim_extinction_text = anim_extinction_text_p;
        anim_extinction_image = anim_extinction_image_p;
        anim_transition_arrow = anim_transition_arrow_p;
        anim_extinction_info = anim_extinction_info_p;
        anim_extinction_icon = anim_extinction_icon_p;

        Extinction_Text_Info = Extinction_Text_Info_p;
        extinction_text = extinction_text_p;

        Continue_Button = Continue_Button_p;

        example_text = example_text_p;
        example_extinction_text = example_extinction_text_p;
    }

    private void Start()
    {
        Set_Extinction_Time();
    }

    static int random_extinction = 0;

    public static void Display_First_Extinction_Information()
    {
        

        CameraManager.Switch_Camera(Camera_Types.ExtinctionCamera);

        timer_count = true;
    }

    public static void Display_Extinction_Information()
    {
        if (timer_count) return;

        timer_count = true;
        current_arrow_pointer++;
        AudioManager.Play_SFX(SFX.EX_CosmicScale);
        AudioManager.Play_SFX(SFX.EX_TickTock);

        anim_extinction_info.SetBool("Active", true);
        anim_extinction_text.SetInteger("ExtinctionText", random_extinction);
        TimeAnimator.Increase_Time();
        //PlayerInput.Add_To_Player_Input(Extinction_Input);

        max_time = 4;
    }

    static bool timer_count = false;
    static int current_arrow_pointer = 0;
    static float max_time = 5;
    float current_time = 0;
    bool first_timer = false;
    bool played_sfx = false;

    bool arrow_sfx = false;

    public void Update()
    {
        if (!timer_count) return;

        current_time += Time.deltaTime;

        if(current_time >= max_time)
        {
            timer_count = false;
            anim_extinction_info.SetBool("Active", false);

            if (GlobalMinigameManager.Last_Minigame())
            {
                Display_Final_Extinction_Information();
                return;
            }

            if (!first_timer)
            {
                first_timer = true;
                Continue_Button.SetActive(true);
                Continue_Button.GetComponent<Animator>().SetBool("Active", true);
                return;
            }
            else 
            {
                arrow_sfx = false;
                Continue_Button.SetActive(false); 
            }

                Continue();
        } else if (current_time >= max_time / 2)
        {
            if (first_timer)
            {
                anim_transition_arrow.SetInteger("Arrow", current_arrow_pointer);
                if(!arrow_sfx)
                {
                    arrow_sfx = true;
                    AudioManager.Play_SFX(SFX.EX_ArrowLtoR);
                }
            }

        }

        else if (!played_sfx)
        {
            if (extinction_event == Extinction_Event.Ice_Age) AudioManager.Play_SFX(SFX.SS_IceAge);
            else AudioManager.Play_SFX(SFX.SS_GlobalWarming);

            played_sfx = true;
        }
    }

    public void Continue()
    {
        AudioManager.Play_SFX(SFX.SS_ContinueButton);

        if (extinction_event == Extinction_Event.Ice_Age) AudioManager.Play_SFX(SFX.SS_IceAge, false);
        else AudioManager.Play_SFX(SFX.SS_GlobalWarming, false);

        anim_extinction_icon.SetInteger("ExtinctionType", random_extinction + 1);

        Continue_Button.GetComponent<Animator>().SetBool("Active", false);

        CameraManager.Switch_Camera(Camera_Types.MainCamera);
        Continue_Minigame();

        current_time = 0;
    }

    //public static void Extinction_Input(Touch touch)
    //{
    //    if (touch.phase == TouchPhase.Began)
    //    {
    //        if (GlobalMinigameManager.Last_Minigame()) Display_Final_Extinction_Information();
    //        else Continue_Minigame();
    //    }
    //}

    public static void Continue_Minigame()
    {
        GlobalMinigameManager.Start_MiniGame();

       // PlayerInput.Remove_From_Player_Input(Extinction_Input); 
    }

    public static void Display_Final_Extinction_Information()
    {
        print("DisplayInput");

        Generate_Text();

        CameraManager.Switch_Camera(Camera_Types.ExtinctionCamera);

        PlayerInput.Reset_Input();
    }

    public static void Reset_Game_After_Final_Extinction(Touch touch)
    {
        if (touch.phase == TouchPhase.Began) RestartManager.Restart();
    }

    public void Set_Extinction_Time()
    {
        random_extinction = Random.RandomRange(0, extinction_range);
        extinction_event = (Extinction_Event)random_extinction;

        string _extinction_text = "In 500 million years, a great " + extinction_event.ToString() + " will come. Will your species survive?";
        _extinction_text = _extinction_text.Replace("_", " ");
        for(var i = 0; i < extinction_text.Length; i++) extinction_text[i].text = _extinction_text;
        for (var i = 0; i < example_extinction_text.Length; i++) example_extinction_text[i].text = string.Empty;

        anim_extinction_text.SetInteger("ExtinctionText", random_extinction);
        anim_extinction_image.SetInteger("Extinction", random_extinction);

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

    public static void Generate_Text()
    {
        List<Evolution> _final_evolutions = EvolutionManager.Get_Evolutions();

        int _calulating_score = 0;
        string _score_text = string.Empty;
        string _good_text = string.Empty;
        string _bad_text = string.Empty;
        string _overall = string.Empty;

        for(var i = 0; i < Extinction_Text_Info.Count; i++)
        {
            if (!_final_evolutions.Contains(Extinction_Text_Info[i].evolution_parameter)) continue;

            Evolution_Individual_Information[] extinction_info = null;

            if (random_extinction == 0) extinction_info = Extinction_Text_Info[i].GlobalWarmingParameters.individual_information;
            else extinction_info = Extinction_Text_Info[i].GlobalWarmingParameters.individual_information;

            Evolution_Individual_Information current_extinction_information = null;

            if (extinction_info.Length <= 0) continue;
            if (extinction_info.Length == 1) current_extinction_information = extinction_info[0];
            else
            {
                int j = 0;

                for(j = 0; j < extinction_info.Length - 1; j++)
                {
                    bool _contains = true;

                    for (var k = 0; k < extinction_info[j].If_These_Parameters.Length; k++)
                    {
                        if (!_final_evolutions.Contains(extinction_info[j].If_These_Parameters[k])) _contains = false;
                    }

                    if (!_contains) continue;
                    else break;
                }

                current_extinction_information = extinction_info[j];
            }

            int _score_change = current_extinction_information.score_change;
            _calulating_score += _score_change;

            if (_score_change > 0)
            {
                if (_good_text != string.Empty) _good_text += " ";
                _good_text += current_extinction_information.EvolutionText;
            } else
            {
                if (_bad_text != string.Empty) _bad_text += " ";
                _bad_text += current_extinction_information.EvolutionText;
            }
        }


        float _score_num = (10 + _calulating_score) / 20;
        _score_num = Mathf.Clamp(_score_num, 0, 1);
        int _final_score = Mathf.RoundToInt(_score_num * 100);

        _score_text = _final_score + "% of your species survied the " + extinction_event.ToString() + "!";

        bool _didnt = false;
        string _overall_text = string.Empty;

        if (_final_score <= (int)Overall_Your_Species_Faired.Didnt)
        {
            _didnt = true;
            _overall = "Overall, your species did not survive the " + extinction_event.ToString() + "...";
        } else if (_final_score <= (int)Overall_Your_Species_Faired.Poorly) _overall_text = Overall_Your_Species_Faired.Poorly.ToString();
        else if (_final_score <= (int)Overall_Your_Species_Faired.Decently) _overall_text = Overall_Your_Species_Faired.Decently.ToString();
        else if (_final_score <= (int)Overall_Your_Species_Faired.Well) _overall_text = Overall_Your_Species_Faired.Well.ToString();
        else if (_final_score <= (int)Overall_Your_Species_Faired.Great) _overall_text = Overall_Your_Species_Faired.Great.ToString();
        else if (_final_score <= (int)Overall_Your_Species_Faired.Excellently) _overall_text = Overall_Your_Species_Faired.Excellently.ToString();

        if (!_didnt) _overall = "Overall, your species faired " + _overall + " in the " + extinction_event.ToString() + ".";

        string _desc_text = string.Empty;
        if(_good_text != string.Empty) _desc_text = _good_text;
        if(_bad_text != string.Empty)
        {
            if (_desc_text != string.Empty) _desc_text += "\n" + "\n" + "But " + _bad_text;
            else _desc_text = _bad_text;
        }

        string _text = _score_text + "\n" + "\n" + _desc_text + "\n" + "\n" + _overall_text;
        _text = _text.Replace("_", " ");
        for (var i = 0; i < extinction_text.Length; i++)
        {
            extinction_text[i].text = _text;
            extinction_text[i].fontSize = 6;
        }

        // Just for milestone build

        for (var i = 0; i < extinction_text.Length; i++)
        {
            extinction_text[i].text = string.Empty;
        }

        for (var i = 0; i < example_extinction_text.Length; i++)
        {
            example_extinction_text[i].text = example_text;
            //extinction_text[i].fontSize = 6;
        }
    }

    [Header("Example Text")]
    public TMP_Text[] example_extinction_text_p;
    static TMP_Text[] example_extinction_text;
    [TextArea(5, 5)]
    public string example_text_p;
    static string example_text;
}

public enum Extinction_Event
{
    Ice_Age,
    Drought
}

public enum Overall_Your_Species_Faired
{
    Didnt = 0,
    Poorly = 15, 
    Decently = 35,
    Well = 55,
    Great = 70, 
    Excellently = 90
}

[System.Serializable]
public class Extinction_Ending_Information
{
    public Evolution evolution_parameter;
    public Evolution_Information GlobalWarmingParameters;
    public Evolution_Information IceAgeParameters;
}


[System.Serializable]
public class Evolution_Information
{
    public Evolution_Individual_Information[] individual_information;
}

[System.Serializable]
public class Evolution_Individual_Information
{
    public Evolution[] If_These_Parameters;
    public int score_change;
    [TextArea(5, 5)]
    public string EvolutionText;
}
