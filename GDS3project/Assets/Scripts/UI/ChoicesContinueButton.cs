using UnityEngine;

public class ChoicesContinueButton : MonoBehaviour
{
    public MinigameThreeManager minigame_3;
    public BoxCollider2D boxcolliderBounds;

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

            if (!bounds.Contains(touch_pos)) return;

            for(var i = 0; i < minigame_3.text_choices.Count; i++)
            {
                Evolution2 _choice = minigame_3.text_choices[i].Get_Current_Choice();

                if(_choice == Evolution2.Null) return;
            }

            minigame_3.End_MiniGame();
        }
    }
}
