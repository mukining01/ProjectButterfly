using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public Animator animator;

    [HeaderAttribute("ItemProperties")]
    public UnityEvent myEvent;

    bool hit = false;

    bool can_interact = false;
    bool interacting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger enter");

        interacting = true;

        if (!collision.GetComponent<CreatureAI>()) return;

        if (hit) return;

        Collect();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interacting = false;
    }

    protected void Set_Can_Interact(bool _can)
    {
        can_interact = _can;

        if(can_interact && interacting && !hit) Collect();
    }

    private void Collect()
    {
        hit = true;

        myEvent.Invoke();

        animator.SetTrigger("Collected");

        FindObjectOfType<CreatureSprite>().Proto_Creature_Eat();
    }
}
