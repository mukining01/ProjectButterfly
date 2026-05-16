using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameOneManager : MinigameManager
{
    public List<GameObject> global_elements = new List<GameObject>();
    public List<EatenBarElement> eaten_food_ui_component = new List<EatenBarElement>();
    int current_amount_of_food_eaten = 0;

    [HeaderAttribute("Crabs")]
    public List<Crab> crab_object = new List<Crab>();
    public List<BoxCollider2D> Crab_Bounds = new List<BoxCollider2D>();

    [HeaderAttribute("Berrys")]
    public List<BerryDrop> berry_object = new List<BerryDrop>();
    public BoxCollider2D Berry_Bounds;

    [HeaderAttribute("OtherFoods")]
    public List<InteractableObject> other_foods = new List<InteractableObject>();
    public List<BoxCollider2D> other_foods_bounds = new List<BoxCollider2D>();

    bool end_minigame = false;

    public void On_Meat_Eaten()
    {
        Set_UI(1);

        evolution_Type--;
    }

    public void On_Greens_Eaten()
    {
       Set_UI(2);

        evolution_Type++;
    }

    Coroutine Crabs;
    Coroutine Berrys;

    public void Set_UI(int eaten_type)
    {
        if (end_minigame) return;

        eaten_food_ui_component[current_amount_of_food_eaten].Set_EatenType(eaten_type);
        current_amount_of_food_eaten++;

        if (current_amount_of_food_eaten >= eaten_food_ui_component.Count)
        {
            end_minigame = true;
            StartCoroutine(End_Minigame_Delay());

            if(Crabs != null)
            {
                StopCoroutine(Crabs);
                Crabs = null;
            }

            if (Berrys != null)
            {
                StopCoroutine(Berrys);
                Berrys = null;
            }
        }
    }

    public override void Start_MiniGame()
    {
        base.Start_MiniGame();

        BackgroundManager.Increase_Background_Type();

        evolution_Type = 0;

        for (int i = 0; i < global_elements.Count; i++)
        {
            global_elements[i].SetActive(true);
        }

        for (int i = 0; i < eaten_food_ui_component.Count; i++)
        {
            eaten_food_ui_component[i].Set_EatenType(0);
        }

        Crabs = StartCoroutine(Spawn_Crab_Timer());
        Berrys = StartCoroutine(Spawn_Berry_Timer());
        Set_All_Starting_Interactable_Bounds();
    }

    public override void End_MiniGame()
    {
        print("end minigame");

        if (evolution_Type < 0) evolution_Type = 1;
        else evolution_Type = 2;

            for (int i = 0; i < global_elements.Count; i++)
            {
                global_elements[i].SetActive(false);
            }



        for (int i = 0; i < crab_object.Count; i++)
        {
            crab_object[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < berry_object.Count; i++)
        {
            berry_object[i].gameObject.SetActive(false);
        }

        EvolutionManager.Set_Evolution(1, evolution_Type);

        base.End_MiniGame();

        //

    }

    int crab_count = 0;

    public IEnumerator Spawn_Crab_Timer()
    {
        yield return new WaitForSeconds(6);

        Spawn_Crab();
    }

    public void Spawn_Crab()
    {
        int rand = Random.Range(1, 3);

        int start_direction = rand;
        if (start_direction == 2) start_direction = -1;
        crab_object[crab_count].Change_Start_Direction(start_direction);

        Bounds bounds = Crab_Bounds[rand - 1].bounds;

        crab_object[crab_count].Set_Pos(StartingMovementInBounds(bounds));

        crab_count++;
        if (crab_count >= crab_object.Count) return;

        if(Crabs != null) StopCoroutine(Crabs);
        Crabs = StartCoroutine(Spawn_Crab_Timer());
    }

    int berry_count = 0;

    public IEnumerator Spawn_Berry_Timer()
    {
        yield return new WaitForSeconds(4);

        Spawn_Berry();
    }

    public void Spawn_Berry()
    {
        berry_object[berry_count].Change_Start_Fall();

        Bounds bounds = Berry_Bounds.bounds;

        berry_object[berry_count].Set_Pos(StartingMovementInBounds(bounds));

        berry_count++;
        if (berry_count >= berry_object.Count) return;

        if (Berrys != null) StopCoroutine(Berrys);
        Berrys = StartCoroutine(Spawn_Berry_Timer());
    }

    public void Set_All_Starting_Interactable_Bounds()
    {
        for(var i = 0; i < other_foods.Count; i++)
        {
            int rand = Random.Range(1, 3);
            Bounds bounds = other_foods_bounds[rand - 1].bounds;

            other_foods[i].transform.position = StartingMovementInBounds(bounds);
            other_foods[i].gameObject.SetActive(true);
        }
    }

    public static Vector2 StartingMovementInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
