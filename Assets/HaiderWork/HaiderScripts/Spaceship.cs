using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            anim.SetBool("Open", true);
            anim.SetBool("Close", false);
            isOpen = true;
        }
        else
        {
            anim.SetBool("Close", true);
            anim.SetBool("Open", false);
            isOpen = false;
        }
    }
}
