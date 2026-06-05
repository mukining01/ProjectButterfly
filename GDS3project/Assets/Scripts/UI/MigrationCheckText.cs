using UnityEngine;

public class MigrationCheckText : MonoBehaviour
{
    public Animator text_appear;
    public Animator migration_type;
    public MinigameTwoManager MinigameTwoManager;

    int type;

    public void Pass_Migration_Type(int _type)
    {
        type = _type;

        text_appear.SetBool("Appear", true);
        migration_type.SetInteger("Type", type);

        MinigameTwoManager.Freeze_Player();

        AudioManager.Play_SFX(SFX.M3_Interact);
    }

    public void Yes_Input()
    {
        MinigameTwoManager.Set_Evolution_Type(type);
        End();


        AudioManager.Play_SFX(SFX.M3_Interact);
    }

    public void No_Input()
    {
        MinigameTwoManager.Reset_Player();
        type = 0;
        End();


        AudioManager.Play_SFX(SFX.M3_Interact);
    }

    public void End()
    {
        text_appear.SetBool("Appear", false);
        //migration_type.SetInteger("Type", type);
    }
}
