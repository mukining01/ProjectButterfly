using UnityEngine;

public class ChoicesContinueButton : MonoBehaviour
{
    public MinigameThreeManager minigame_3;
    public BoxCollider2D boxcolliderBounds;

    Animator animator;

    public void Start()
    {
        //input

        animator = GetComponent<Animator>();
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

            if (!bounds.Contains(touch_pos)) return;

            for(var i = 0; i < minigame_3.text_choices.Count; i++)
            {
                Evolution _choice = minigame_3.text_choices[i].Get_Current_Choice();

                if(_choice == Evolution.Null) return;
            }

            minigame_3.End_MiniGame();
        }
    }

    int[] choices = new int[3];

    public void Set_Int(int _value, int _set)
    {
        choices[_value] = _set;

        if (choices[0] == 1 && choices[1] == 1 && choices[2] == 1) animator.SetBool("Appear", true);
        else animator.SetBool("Appear", false);
    }
}
