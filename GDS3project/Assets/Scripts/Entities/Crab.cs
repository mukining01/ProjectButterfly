using UnityEngine;

public class Crab : InteractableObject
{
    float direction = 1;
    float walk_distance = 21;
    float overall_position_change = 0;
    float position_change = 0.01f;

    public void Update()
    {
        Increase_Position();
    }

    public void Increase_Position()
    {
        overall_position_change += position_change;
        transform.position = new Vector2(transform.position.x + (position_change * direction), transform.position.y);

        if (overall_position_change >= walk_distance)
        {
            Change_Start_Direction(direction * -1);
        }
    }

    public void Change_Start_Direction(float _new_direction = 0)
    {
        overall_position_change = 0;
        direction = _new_direction;
        print("crab direction: " + _new_direction);
    }

    public void Set_Pos(Vector2 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
