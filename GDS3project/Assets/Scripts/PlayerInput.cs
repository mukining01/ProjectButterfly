using Unity.Cinemachine;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public CreatureAI creature_AI;

    public delegate void PInput(Touch touch);

    public static PInput input = null;

    public Transform player_input_transform;
    public Animator player_input_animator;

    private void Awake()
    {
        //input = null;


        player_input_transform = transform.GetChild(0).transform;
        player_input_animator = player_input_transform.gameObject.GetComponent<Animator>();

        input += Place_Input_Marker;
    }

    public static void Add_To_Player_Input(PInput input_action)
    {
        input += input_action;
    }

    public static void Remove_From_Player_Input(PInput input_action)
    {
        input -= input_action;
    }

    public static void Reset_Input()
    {
        input = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (input == null) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            print(touch);

            input(touch);
        }
    }



    public void Place_Input_Marker(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            player_input_transform.position = CameraManager.Get_Base_Camera().ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));

            player_input_animator.SetTrigger("Touch");
        }
    }
}
