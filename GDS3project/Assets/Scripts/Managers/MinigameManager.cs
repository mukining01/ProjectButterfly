using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public int evolution_Type;

    public virtual void Start_MiniGame() { }

    public virtual void End_MiniGame()
    {
        GlobalMinigameManager.End_MiniGame();
        EvolutionManager.Post_Minigame_Evolution(evolution_Type);
    }

    protected IEnumerator End_Minigame_Delay()
    {
        yield return new WaitForSeconds(0.1f);

        End_MiniGame();
    }
}
