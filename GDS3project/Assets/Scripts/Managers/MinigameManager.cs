using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public CreatureAI creature_ai;
    public int evolution_Type;

    public virtual void Start_MiniGame()
    {
        AudioManager.Play_SFX(SFX.M1_LevelStart);

        creature_ai.Idle_Activation(false);
        creature_ai.Set_Transform_Zero();
        creature_ai.Add_To_Player_Input(true);
        creature_ai.Renable_For_MiniGames();
    }

    public virtual void End_MiniGame()
    {
        AudioManager.Play_SFX(SFX.M1_LevelComplete);

        creature_ai.Add_To_Player_Input(false);

        GlobalMinigameManager.End_MiniGame();

        if (!GlobalMinigameManager.Last_Minigame()) EvolutionManager.Post_Minigame_Evolution(evolution_Type);
        else ExtinctionManager.Display_Extinction_Information();
    }

    protected IEnumerator End_Minigame_Delay()
    {
        yield return new WaitForSeconds(0.1f);

        End_MiniGame();
    }
}
