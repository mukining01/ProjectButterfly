using UnityEngine;

public class TwoChoiceText : MonoBehaviour
{
    public BoxCollider2D boxcolliderBounds;

    public Animator Input_Screen;
    int increment = 0;

    public Evolution current_choice = Evolution.Null;
    public Evolution choice_1;
    public Evolution choice_2;

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

            increment += _increment;
            increment = Mathf.Clamp(increment, -1, 1);
            Input_Screen.SetInteger("Choice", increment);

            if (increment == 1) current_choice = choice_1;
            else if (increment == -1) current_choice = choice_2;
            else current_choice = Evolution.Null;
        }
            
    }

    public Evolution Get_Current_Choice()
    {
        return current_choice;
    }
}
