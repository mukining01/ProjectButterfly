using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    public GameObject Evolution_UI_p;
    static GameObject Evolution_UI;

    static CreatureAI creatureAI;
    static CreatureSprite creatureimage;

    public void Awake()
    {
        creatureimage = FindObjectOfType<CreatureSprite>().GetComponent<CreatureSprite>();
        creatureAI = FindObjectOfType<CreatureAI>().GetComponent<CreatureAI>();

        Evolution_UI = Evolution_UI_p;
        Evolution_UI.SetActive(false);
    }

    public static void Post_Minigame_Evolution(int evolution_type)
    {
        CameraManager.Switch_Camera(Camera_Types.Evolution_Camera_0);
        ExtinctionManager.Set_New_Extinction_Time();

        creatureimage.Set_Creature_Evolving(evolution_type);
        creatureAI.Setting_Static();
    }

    public static void Post_Creature_Evolution()
    {
        CameraManager.Switch_Camera(Camera_Types.Evolution_Camera_1);

        Evolution_UI.SetActive(true);
    }

    public static void Continue_After_Evolution_Manager()
    {
        CameraManager.Switch_Camera(Camera_Types.MainCamera);

        Evolution_UI.SetActive(false);

        ExtinctionManager.Display_Extinction_Information();
    }
}

[System.Serializable]
public class Creature_Sprites
{
    public Evolution1 evolution_one;
    public Evolution1 evolution_two;
    public Evolution1 evolution_three;
}

public enum Evolution1
{
    Null, Omnivore, Carnivore
}

public enum Evolution2
{
    Null, Choice_1, Choice_2, Choice_3, Choice_4, Choice_5, Choice_6
}

public enum Evolution3
{
    Null, Mountains, Dessert, Forest, Sea
}
