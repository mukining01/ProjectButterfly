using UnityEngine;

public class BerryDrop : InteractableObject
{
    float direction = 1;
    float fall_distance = 0;
    float overall_position_change = 0;
    float position_change = 0.1f;

    bool stop_drop = false;

    public void Update()
    {
        Increase_Position();
    }

    public void Increase_Position()
    {
        if (stop_drop) return;

        overall_position_change += position_change;
        transform.position = new Vector2(transform.position.x, transform.position.y - position_change);

        if (overall_position_change >= fall_distance)
        {
            stop_drop = true;
        }
    }

    public void Change_Start_Fall()
    {
        fall_distance = Random.Range(8, 15);
    }

    public void Set_Pos(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
