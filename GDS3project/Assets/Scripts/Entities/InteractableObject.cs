using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public Animator animator;

    [HeaderAttribute("ItemProperties")]
    public UnityEvent myEvent;

    bool hit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger enter");

        if (!collision.GetComponent<CreatureAI>()) return;

        if (hit) return;

        hit = true;

        myEvent.Invoke();

        animator.SetTrigger("Collected");
    }
}
