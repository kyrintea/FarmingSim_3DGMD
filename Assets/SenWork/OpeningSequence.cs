using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningSequence : MonoBehaviour
{
    private Animator daAnimator;
    private int counter;

    public AudioSource soundSource;
    public AudioClip explosion;

    private void Start()
    {
        counter = 1;
        daAnimator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (counter >= 7)
        {
            SceneManager.LoadScene("TitleScreen");
        }

        if (counter == 6)
        {
            soundSource.PlayOneShot(explosion);
        }
    }
    public void ImageButton()
    {

        daAnimator.Play("Anim" + counter);
        counter++;
        Debug.Log("button1clicked");
        Debug.Log(counter);
    }
}
