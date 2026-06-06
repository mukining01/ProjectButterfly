using Pathfinding;
using System.Collections;
using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    [Header("Creature AI values")]
    public float movement_speed = 0.0f;
    public float next_waypoint_distance = 0.0f;
    public float distance_until_stop = 0.0f;


    Rigidbody2D rb;

    [Header("Other Creature Values")]
    public Transform target;
    public CreatureSprite creature_sprite;
    public Transform creature_sprite_transform;
    float update_path_delay = 0.5f;
    public bool added_to_input = false;
    public BoxCollider2D starting_movement_bounds;

    Path path;
    int currentWayPoint;
    bool reachedEndOfPath;

    Seeker seeker;

    bool idling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        //Start_Update_Path();
        //InvokeRepeating("UpdatePath", 0f, 0.5f);

        Idle_Activation(true);


        Add_To_Player_Input(true);
    }

    public void Start()
    {
       
    }

    public void UpdatePath()
    {
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void Update()
    {
        bool _moving = Moving();

        creature_sprite.Set_Creature_Moving(_moving);

        if(sfx_playing && !_moving)
        {
            AudioManager.Play_SFX(SFX.W_SandWalking, false);
            AudioManager.Play_SFX(SFX.W_BigWalking, false);
            sfx_playing = false;
        }
        else if (!sfx_playing && _moving)
        {
            if(!GlobalMinigameManager.Final_Minigame()) AudioManager.Play_SFX(SFX.W_SandWalking);
            else AudioManager.Play_SFX(SFX.W_BigWalking);
            sfx_playing = true;
        }
    }

    bool sfx_playing = false;

    public bool Moving()
    {
        if (!added_to_input) return false;
        if (is_static) return false;
        if (path == null)
            return false;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            creature_sprite.Set_Creature_Moving(false);
            reachedEndOfPath = true;
            return false;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distane_to_target = Vector2.Distance(rb.position, target.localPosition);

        if (distane_to_target < distance_until_stop) return false; 

        creature_sprite.Set_Creature_Moving(true);

        float _movement_speed = movement_speed;
        if (setting_static)
        {
            current_time_taken_until_stop -= Time.deltaTime;
            if (current_time_taken_until_stop <= 0)
            {
                Set_Static_For_Extinction_Manager();
                return false;
            }
            _movement_speed *= (current_time_taken_until_stop / time_taken_until_stop);
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * _movement_speed * Time.deltaTime;

        // rb.AddForce(force);
        //transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWayPoint], (movement_speed / distane) * Time.deltaTime);

        transform.localPosition = new Vector3(transform.localPosition.x + force.x, transform.localPosition.y + force.y, 0);

        if (force.x > 0) creature_sprite_transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (force.x < 0) creature_sprite_transform.localScale = new Vector3(1f, 1f, 1f);

        float distane = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distane < next_waypoint_distance)
        {
            currentWayPoint++;
        }



        ////Disregard below if you dont want the sprite to flip to look at the player.
        ////You can change 'force' to 'rb.velocity' if you want the sprite to flip depending on the velocity and not the direction it is travelling toward the player.
        //if (force.x >= 0.01f)
        //{
        //    enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        //}
        //else if (force.x <= -0.01f)
        //{
        //    enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        //}

        return true;
    }

    public void Update_Path_On_Input(Touch touch)
    {
        print("years ago 1");

        if (touch.phase == TouchPhase.Began)
        {
            Vector2 _start_pos = CameraManager.Get_Base_Camera().ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));
            Set_Target_Position(_start_pos);
        }
    }

    public void Reset_Seeking_Position()
    {
        Set_Target_Position(transform.position);
    }

    public void Set_Target_Position(Vector2 target_position)
    {
        target.position = target_position;

        UpdatePath();
    }

    public void Add_To_Player_Input(bool _input_player)
    {
        if(_input_player) PlayerInput.Add_To_Player_Input(Update_Path_On_Input);
        else PlayerInput.Remove_From_Player_Input(Update_Path_On_Input);

        added_to_input = _input_player;
    }

    Coroutine idle_moving = null;

    public void Idle_Activation(bool _set_idle)
    {
        idling = _set_idle;

        if(idling)
        {
            idle_moving = StartCoroutine(Idle_Moving());
        } else
        {
            if(idle_moving != null)
            {
                StopCoroutine(idle_moving);
                Reset_Seeking_Position();
            }
        }
    }

    public void Set_Transform_Zero()
    {
        transform.position = Vector3.zero;
        creature_sprite_transform.localScale = new Vector3(1f, 1f, 1f);
    }

    bool is_static = false;
    Vector2 original_transform;
    Vector2 original_scale;

    bool setting_static = false;
    float time_taken_until_stop = 0.5f;
    float current_time_taken_until_stop;

    public void Setting_Static()
    {
        setting_static = true;
        current_time_taken_until_stop = time_taken_until_stop;
    }

    public void Set_Static_For_Extinction_Manager()
    {
        setting_static = false;

        //original_transform = transform.position;
        //original_scale = transform.localScale;

        //transform.position = Vector3.zero;
       // transform.localScale = Vector3.one;

        PlayerInput.Remove_From_Player_Input(Update_Path_On_Input);

        is_static = true;
    }

    public void Renable_For_MiniGames()
    {
        current_time_taken_until_stop = time_taken_until_stop;
        setting_static = false;

        //transform.position = original_transform;
        //transform.localScale = original_scale;

        PlayerInput.Add_To_Player_Input(Update_Path_On_Input);

        is_static = false;
        Set_Target_Position(Vector3.zero);
    }

    //

    public IEnumerator Idle_Moving()
    {
        Vector2 _starting_position = StartingMovementInBounds(starting_movement_bounds.bounds, starting_movement_bounds.transform.localPosition);
        Set_Target_Position(_starting_position);

        float _random_time = Random.Range(4, 6);

        yield return new WaitForSeconds(_random_time);

        idle_moving = StartCoroutine(Idle_Moving());
    }

    public static Vector2 StartingMovementInBounds(Bounds bounds, Vector2 pos)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x) + pos.x,
            Random.Range(bounds.min.y, bounds.max.y) + pos.y
        );
    }
}
