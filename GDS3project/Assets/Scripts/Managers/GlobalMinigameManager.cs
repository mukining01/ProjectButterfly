using UnityEngine;

public class GlobalMinigameManager : MonoBehaviour
{
    public MinigameManager[] MiniGames_p;
    public static MinigameManager[] MiniGames;

    static int current_minigame = 0;

    private void Awake()
    {
        MiniGames = MiniGames_p;

        current_minigame = 0;
    }

    public static void Start_MiniGame()
    {
        MiniGames[current_minigame].Start_MiniGame();
    }

    public static void End_MiniGame()
    {
        current_minigame++;
    }

    public static bool Last_Minigame()
    {
        if (current_minigame > MiniGames.Length - 1) return true;
        return false;
    }

    public static bool Final_Minigame()
    {
        if (current_minigame >= MiniGames.Length - 1) return true;
        return false;
    }

    public static int Get_Minigame_Amount()
    {
        return MiniGames.Length;
    }
}
