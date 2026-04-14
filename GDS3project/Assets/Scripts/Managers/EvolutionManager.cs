using UnityEngine;

public class EvolutionManager : MonoBehaviour
{
    static CreatureAI creatureAI;
    static CreatureSprite creatureimage;

    public void Awake()
    {
        creatureimage = FindObjectOfType<CreatureSprite>().GetComponent<CreatureSprite>();
        creatureAI = FindObjectOfType<CreatureAI>().GetComponent<CreatureAI>();
    }

    public static void Post_Minigame_Evolution(int evolution_type)
    {
        creatureimage.Set_Evolution(evolution_type);

        creatureAI.Set_Static_For_Extinction_Manager();

        PlayerInput.Add_To_Player_Input(Continue_After_Evolution_Manager);
    }

    public static void Continue_After_Evolution_Manager(Touch touch)
    {
        if (touch.phase != TouchPhase.Began) return;

        PlayerInput.Remove_From_Player_Input(Continue_After_Evolution_Manager);

        if (GlobalMinigameManager.Last_Minigame()) ExtinctionManager.Display_Final_Extinction_Information(creatureAI);
        else GlobalMinigameManager.Start_MiniGame();
    }
}
