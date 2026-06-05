using UnityEngine;

public class TwoChoiceText : MonoBehaviour
{
    public Collider2D boxcolliderBounds;

    public Animator Input_Screen;
    int increment = 0;

    public Evolution current_choice = Evolution.Null;
    public Evolution choice_1;
    public Evolution choice_2;

    public CreatureSpriteAdaptions sprite_adapations;
    public int choice = 0;
    public ChoicesContinueButton choiceContinueButton;


    public void Start()
    {
        //input
    }

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
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 touch_pos = CameraManager.Get_Base_Camera().ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));

            Bounds bounds = boxcolliderBounds.bounds;

            int _increment = 1;

            print("touch position: " + touch_pos + ", center of bounds: " + bounds.center);

            if (bounds.Contains(touch_pos))
            {
                if (touch_pos.y < bounds.center.y) _increment = -1;
            }
            else return;

            increment = _increment;
            increment = Mathf.Clamp(increment, -1, 1);
            Input_Screen.SetInteger("Choice", increment);

            bool clear = false;

            if (increment == 1) current_choice = choice_1;
            else if (increment == -1) current_choice = choice_2;
            else clear = true;

            bool _carnivore = FindAnyObjectByType<CreatureSprite>().Get_Is_Carnivore();

            sprite_adapations.Set_Evolution_Active(current_choice, choice, _carnivore);

            if (clear) choiceContinueButton.Set_Int(choice, 0);
            else choiceContinueButton.Set_Int(choice, 1);

            AudioManager.Play_SFX(SFX.M2_ChoiceSelection);
        }
            
    }

    public Evolution Get_Current_Choice()
    {
        return current_choice;
    }
}
