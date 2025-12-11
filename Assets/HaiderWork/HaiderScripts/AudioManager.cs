using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Boost;
    public AudioSource Eating;
    public AudioSource MetalDoor;
    public AudioSource Walking;
    public AudioSource Wind;
    public AudioSource CantGrabMetal;
    public AudioSource MetalCollected;
    public AudioSource NotEnoughMetal;

    public void boost()
    {
        Boost.Play();
    }

    public void eating()
    {
        Eating.Play();
    }

    public void metalDoor()
    {
        MetalDoor.Play();
    }

    public void walking()
    {
        Walking.Play();
    }

    public void wind()
    {
        Wind.Play();
    }

    public void cantGrabMetal()
    {
        CantGrabMetal.Play();
    }

    public void metalCollected()
    {
        MetalCollected.Play();
    }

    public void notEnoughMetal()
    {
        NotEnoughMetal.Play();
    }
}
