using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTwoManager : MinigameManager
{
    public GameObject Map = null;
    public GameObject Interactable_points = null;

    private void Awake()
    {
        Interactable_points.SetActive(true);
    }

    public override void Start_MiniGame()
    {
        base.Start_MiniGame();

        evolution_Type = 0;

        Interactable_points.SetActive(true);

        Map.SetActive(true);

        CameraManager.Switch_Camera(Camera_Types.SmallMap);

        StartCoroutine(Delay_Before_LargeMap());
    }

    IEnumerator Delay_Before_LargeMap()
    {
        yield return new WaitForSeconds(3f);

        CameraManager.Switch_Camera(Camera_Types.LargeMap);
    }

    public void Set_Evolution_Type(int evolution_type)
    {
        evolution_Type = evolution_type;

        StartCoroutine(End_Minigame_Delay());
    }

    public override void End_MiniGame()
    {
        print("end minigame");

        Interactable_points.SetActive(false);
        base.End_MiniGame();
    }
}
