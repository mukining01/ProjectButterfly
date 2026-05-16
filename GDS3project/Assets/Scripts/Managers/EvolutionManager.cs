using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class EvolutionManager : MonoBehaviour
{
    static CreatureAI creatureAI;
    static CreatureSprite creatureimage;

    public Animator EvolutionMangagerAnimator_p;
    static Animator EvolutionMangagerAnimator;

    static List<Evolution> Current_Evolutions = new List<Evolution>();

    public void Awake()
    {
        creatureimage = FindObjectOfType<CreatureSprite>().GetComponent<CreatureSprite>();
        creatureAI = FindObjectOfType<CreatureAI>().GetComponent<CreatureAI>();

        EvolutionMangagerAnimator = EvolutionMangagerAnimator_p;
    }

    public static void Post_Minigame_Evolution(int evolution_type)
    {
        CameraManager.Switch_Camera(Camera_Types.Evolution_Camera_0);
        ExtinctionManager.Set_New_Extinction_Time();

        creatureimage.Set_Creature_Evolving(evolution_type);
        creatureAI.Setting_Static();

        //EvolutionMangagerAnimator.SetInteger("Active", 1);
    }

    public static void Post_Creature_Evolution()
    {
        CameraManager.Switch_Camera(Camera_Types.Evolution_Camera_1);

        EvolutionMangagerAnimator.SetInteger("Active", 2);
    }

    public static void Continue_After_Evolution_Manager()
    {
        EvolutionMangagerAnimator.SetInteger("Active", 0);

        ExtinctionManager.Display_Extinction_Information();
    }

    public static void Set_Evolution(int evolutionType, int evolution)
    {
        int _evolution = evolutionType + (evolutionType * 10) - 10;
        Current_Evolutions.Add((Evolution)_evolution);
    }

    public static List<Evolution> Get_Evolutions()
    {
        return Current_Evolutions;
    }
}

[System.Serializable]
public class Creature_Sprites
{
    public Evolution evolution_one;
    public Evolution evolution_two;
    public Evolution evolution_three;
}

public enum Evolution
{
    Null,
    Omnivore = 01, Carnivore = 02,

    Hump = 11, Fur = 12, Wings = 13,
    Hooves = 14, Paws = 15, Fins = 16,
    Grow = 17, Shrink = 18,

    Mountains = 21, Dessert = 22, Forest = 23, Sea = 24
}

