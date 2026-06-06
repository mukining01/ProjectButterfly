using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSouce_Var[] SFX_p;
    static AudioSouce_Var[] SFX;

    public AudioListener audio_listner_p;
    static AudioListener audio_listner;

    public Animator muter_p;
    static Animator muter;

    public void Awake()
    {
        SFX = SFX_p;
        audio_listner = audio_listner_p;
        muter = muter_p;
    }

    bool mute = false;
    public void Set_Muter()
    {
        mute = !mute;

        audio_listner.enabled = !mute;
        muter.SetBool("Mute", mute);
    }

    public static void Play_SFX(SFX _sfx, bool _play = true)
    {
        int _sfx_int = (int)_sfx;

        if (_sfx_int >= SFX.Length) return;

        AudioSource sfx = SFX[_sfx_int].source;

        float _pitch = Random.RandomRange(SFX[_sfx_int].pitch_min, SFX[_sfx_int].pitch_max);
        sfx.pitch = _pitch;

        if (sfx == null) return;

        if(_play) sfx.Play();
        else sfx.Stop();
    }
}

[System.Serializable]
public class AudioSouce_Var
{
    public AudioSource source;
    public float pitch_min = 1;
    public float pitch_max = 1;
} 

public enum SFX
{
    //Start Screen
    SS_BackgroundMusic = 0,
    SS_StartButton = 1,
    SS_BubbleMoveTap = 2,
    SS_TransitionUP = 3,
    SS_GlobalWarming = 4,
    SS_IceAge = 5,
    SS_ContinueButton = 6,

    //Minigame 1
    M1_LevelStart = 7,
    M1_BackgroundMusic = 8,
    M1_EatVegetables = 9,
    M1_EatMeat = 10,
    M1_Herbivore = 11,
    M1_Carnivore = 12,
    M1_LevelComplete = 13,

    //Evolution Screen
    ES_VineMovement = 14,
    ES_TrailS1 = 15,
    ES_TrailS2 = 16,
    ES_PinningTrait = 17,
    ES_ContinueButton = 18,
    ES_VineMovement_2 = 19,
    ES_VineMovement_3 = 20,

    //Extinction Screen
    EX_CosmicScale = 21,
    EX_TickTock = 22,
    EX_ArrowLtoR = 23,

    //Minigame2
    M2_BackgroundMusic = 24,
    M2_ChoiceSelection = 25,
    M2_ContinueButton = 26,

    //Minigame3
    M3_BackgroundMusic = 27,
    M3_Interact = 28,

    //Walking
    W_SandWalking = 29,
    W_BigWalking = 30,
}
