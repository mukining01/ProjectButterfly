using Pathfinding;
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
    float update_path_delay = 0.5f;

    Path path;
    int currentWayPoint;
    bool reachedEndOfPath;

    Seeker seeker;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        //Start_Update_Path();
        //InvokeRepeating("UpdatePath", 0f, 0.5f);
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
        if (is_static) return;
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distane_to_target = Vector2.Distance(rb.position, target.position);

        if (distane_to_target < distance_until_stop) return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * movement_speed * Time.deltaTime;

        // rb.AddForce(force);
        //transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWayPoint], (movement_speed / distane) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x + force.x, transform.position.y + force.y, 0);

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
    }

    public void Update_Path_On_Input(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            target.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));

            UpdatePath();
        }
    }

    public void OnEnable()
    {
        PlayerInput.Add_To_Player_Input(Update_Path_On_Input);
    }

    public void OnDisable()
    {
        PlayerInput.Remove_From_Player_Input(Update_Path_On_Input);
    }

    bool is_static = false;
    Vector2 original_transform;
    Vector2 original_scale;
    public void Set_Static_For_Extinction_Manager()
    {
        original_transform = transform.position;
        original_scale = transform.localScale;

        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;

        PlayerInput.Remove_From_Player_Input(Update_Path_On_Input);

        is_static = true;
    }

    public void Renable_For_MiniGames()
    {
        transform.position = original_transform;
        transform.localScale = original_scale;

        PlayerInput.Add_To_Player_Input(Update_Path_On_Input);

        is_static = false;
    }
}
