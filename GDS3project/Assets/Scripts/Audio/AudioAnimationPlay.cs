using UnityEngine;

public class AudioAnimationPlay : MonoBehaviour
{
    public bool Appear = false;
    bool played = false;
    public bool start_play = false;
    public bool dont_reset = false;

    public SFX sound;

    public void Start()
    {
        played = false;
        if (start_play) Appear = true;
    }

    public void OnEnable()
    {
        played = false;
        if (start_play) Appear = true;
    }

    private void Update()
    {
        if (Appear && !played)
        {
            played = true;
            AudioManager.Play_SFX(sound);
        }
        else if (!Appear && !dont_reset) played = false;
    }
}
