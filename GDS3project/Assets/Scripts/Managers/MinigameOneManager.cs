using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOneManager : MinigameManager
{
    public List<GameObject> global_elements = new List<GameObject>();
    public List<EatenBarElement> eaten_food_ui_component = new List<EatenBarElement>();
    int current_amount_of_food_eaten = 0;

    

    public void On_Meat_Eaten()
    {
        Set_UI(1);

        if (evolution_Type == 2 || evolution_Type == 3) evolution_Type = 3;
        else evolution_Type = 1;
    }

    public void On_Greens_Eaten()
    {
       Set_UI(2);

        if (evolution_Type == 1 || evolution_Type == 3) evolution_Type = 3;
        else evolution_Type = 2;
    }

    public void Set_UI(int eaten_type)
    {
        eaten_food_ui_component[current_amount_of_food_eaten].Set_EatenType(eaten_type);
        current_amount_of_food_eaten++;

        if (current_amount_of_food_eaten >= eaten_food_ui_component.Count)
        {
            End_MiniGame();
        }
    }

    public override void Start_MiniGame()
    {
        base.Start_MiniGame();

        evolution_Type = 0;

        for (int i = 0; i < global_elements.Count; i++)
        {
            global_elements[i].SetActive(true);
        }

        for (int i = 0; i < eaten_food_ui_component.Count; i++)
        {
            eaten_food_ui_component[i].Set_EatenType(0);
        }
    }

    public override void End_MiniGame()
    {
        print("end minigame");

        for (int i = 0; i < global_elements.Count; i++)
        {
            global_elements[i].SetActive(false);
        }

        base.End_MiniGame();
    }
}
