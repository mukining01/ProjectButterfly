using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTwoManager : MinigameManager
{
    public GameObject Interactable_points = null;

    private void Awake()
    {
        Interactable_points.SetActive(false);
    }

    public override void Start_MiniGame()
    {
        creature_ai.Set_Transform_Zero();
        creature_ai.Add_To_Player_Input(false);

        evolution_Type = 0;

        Interactable_points.SetActive(true);

        BackgroundManager.Increase_Background_Type();

        CameraManager.Switch_Camera(Camera_Types.SmallMap);

        StartCoroutine(Delay_Before_LargeMap());
    }

    IEnumerator Delay_Before_LargeMap()
    {
        yield return new WaitForSeconds(1f);

        CameraManager.Switch_Camera(Camera_Types.LargeMap);
        base.Start_MiniGame();
    }

    public void Freeze_Player()
    {
        creature_ai.Add_To_Player_Input(false);
    }

    public void Reset_Player()
    {
        creature_ai.Add_To_Player_Input(true);
        creature_ai.Set_Transform_Zero();
        creature_ai.Reset_Seeking_Position();
    }

    public void Set_Evolution_Type(int evolution_type)
    {
        evolution_Type = evolution_type;

        StartCoroutine(Delay_Before_Delay_Wow());
    }

    IEnumerator Delay_Before_Delay_Wow()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(End_Minigame_Delay());
    }

    public override void End_MiniGame()
    {
        print("end minigame");

        EvolutionManager.Set_Evolution(3, evolution_Type);

        Interactable_points.SetActive(false);
        base.End_MiniGame();
    }
}
