using System.Collections.Generic;
using UnityEngine;

public class MinigameThreeManager : MinigameManager
{
    [Header("TextChoices")]
    public List<GameObject> text_choices = new List<GameObject>();

    public override void Start_MiniGame()
    {
        base.Start_MiniGame();

        evolution_Type = 0;

        for (int i = 0; i < text_choices.Count; i++)
        {
            text_choices[i].SetActive(true);
        }

        creature_ai.Set_Transform_Zero();
        creature_ai.Add_To_Player_Input(false);
    }

    public override void End_MiniGame()
    {
        print("end minigame");

        for (int i = 0; i < text_choices.Count; i++)
        {
            text_choices[i].SetActive(false);
        }

        base.End_MiniGame();
    }

}
