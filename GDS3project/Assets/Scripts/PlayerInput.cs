using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public CreatureAI creature_AI;
    public Camera mainCamera;

    public delegate void PInput(Touch touch);

    public static PInput input = null;
    public static PInput restart = null;

    public Transform player_input_transform;
    public Animator player_input_animator;

    PlayerInput p_input;
    //PlayerInput p_input;

    private void OnEnable()
    {
        input += Place_Input_Marker;
        //restart += RestartManager.Force_Restart();

        p_input = GetComponent<PlayerInput>();

        //InvokeRepeating("Click", 0.5f, 1);
    }

    public void Click()
    {
        if (input == null) return;

        Touch touch = new Touch();
        touch.position = mouse_position;
        touch.phase = UnityEngine.TouchPhase.Began;

        input(touch);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        print("click a ding");

        if (context.performed)
        {
            mouse_position = context.ReadValue<Vector2>();
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouse_position);
        }
    }

    Vector3 mouse_position;

    public void OnMousePos1(InputValue input)
    {
        Vector3 camdis = new Vector3(input.Get<Vector2>().x, input.Get<Vector2>().y, mainCamera.transform.position.y);
        mouse_position = mainCamera.ScreenToWorldPoint(camdis);
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

    InputAction mouse_input;

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
        if (touch.phase == UnityEngine.TouchPhase.Began)
        {
            player_input_transform.position = CameraManager.Get_Base_Camera().ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));

            player_input_animator.SetTrigger("Touch");
        }
    }
}
