using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class EvolutionManager : MonoBehaviour
{
    static CreatureAI creatureAI;
    static CreatureSprite creatureimage;
    static CreatureSpriteAdaptions creatureSpriteAdapations;

    public Animator EvolutionMangagerAnimator_p;
    static Animator EvolutionMangagerAnimator;

    public Animator EvolutionMangagerAnimator_Path_p;
    static Animator EvolutionMangagerAnimator_Path;

    public Animator[] EvolutionManageerAnimator_Traits_p;
    static Animator[] EvolutionManageerAnimator_Traits;

    static List<Evolution> Current_Evolutions = new List<Evolution>();

    public void Awake()
    {
        creatureimage = FindObjectOfType<CreatureSprite>().GetComponent<CreatureSprite>();
        creatureAI = FindObjectOfType<CreatureAI>().GetComponent<CreatureAI>();
        creatureSpriteAdapations = FindAnyObjectByType<CreatureSpriteAdaptions>().GetComponent<CreatureSpriteAdaptions>();

        EvolutionMangagerAnimator = EvolutionMangagerAnimator_p;
        EvolutionMangagerAnimator_Path = EvolutionMangagerAnimator_Path_p;
        EvolutionManageerAnimator_Traits = EvolutionManageerAnimator_Traits_p;
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
        EvolutionMangagerAnimator_Path.SetBool("Evolute", evolute);

        string evolutions = string.Empty;

        for (var i = 0; i < EvolutionManageerAnimator_Traits.Length; i++)
        {
            EvolutionManageerAnimator_Traits[i].SetInteger("Tag", 0);
        }

        for (var i = 0; i < Current_Evolutions.Count; i++)
        {
            evolutions += Current_Evolutions[i].ToString() + ", ";

            if (i >= EvolutionManageerAnimator_Traits.Length)
            {
                Debug.LogError("To many Evolutions. Evolutions: " + evolutions);
                break;
            }

            EvolutionManageerAnimator_Traits[i].SetInteger("Tag", (int)Current_Evolutions[i]);
            print("Evolution To Tag: " + Current_Evolutions[i] + ", num: " + (int)Current_Evolutions[i]);
        }
    }

    static bool evolute = true;

    public static void Continue_After_Evolution_Manager()
    {
        AudioManager.Play_SFX(SFX.ES_ContinueButton);

        EvolutionMangagerAnimator.SetInteger("Active", 0);
        evolute = false;

        ExtinctionManager.Display_Extinction_Information();
    }

    public static void Set_Evolution(int evolutionType, int evolution)
    {
        if(evolutionType == 2)
        {
            Current_Evolutions.AddRange(creatureSpriteAdapations.Get_Adapations());
            return;
        }

        int _evolution = evolution + (evolutionType * 10) - 10;
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
    Carnivore = 01, Herbivore = 02, 

    Hump = 11, Fur = 12, Wings = 13,
    Hooves = 14, Paws = 15, Fins = 16,
    Grow = 17, Shrink = 18,

    Mountains = 21, Desert = 22, Forest = 23, Sea = 24, Cold = 25
}

