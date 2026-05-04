using UnityEngine;

public class TwoChoiceText : MonoBehaviour
{
    public BoxCollider2D boxcolliderBounds;

    public Animator Input_Screen;
    int increment = 0;

    public Evolution2 current_choice = Evolution2.Null;
    public Evolution2 choice_1;
    public Evolution2 choice_2;

    public void OnEnable()
    {
        PlayerInput.Add_To_Player_Input(OnTextClick);
    }

    public void OnDisable()
    {
        PlayerInput.Remove_From_Player_Input(OnTextClick);
    }

    public void OnTextClick(Touch touch)
    {
        Bounds bounds = boxcolliderBounds.bounds;

        int _increment = 1;

        if(bounds.Contains(touch.position))
        {
            if (touch.position.y < bounds.center.y) _increment = -1;
        }

        increment += _increment;
        Input_Screen.SetInteger("Increment", increment);

        if (increment == 1) current_choice = choice_1;
        else if (increment == -1) current_choice = choice_2;
        else current_choice = Evolution2.Null;
    }
}
