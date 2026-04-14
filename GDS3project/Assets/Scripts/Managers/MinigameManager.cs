using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public int evolution_Type;

    public virtual void Start_MiniGame() { }

    public virtual void On_Interactable_Object_Collected() { }

    public virtual void End_MiniGame()
    {
        GlobalMinigameManager.End_MiniGame();
        EvolutionManager.Post_Minigame_Evolution(evolution_Type);
    }

    public void Pass_Choice_To_Evolution_Manager()
    {

    }
}
