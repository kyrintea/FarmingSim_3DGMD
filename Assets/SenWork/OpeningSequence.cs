using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class OpeningSequence : MonoBehaviour
{
    [Header("Images")]
    public RawImage image1;
    public RawImage image2;
    public RawImage image3;
    public RawImage image4;

    public Image testImage;

    private int counter;

    [Header("Positions")]
    Vector2 startPos1;
    Vector2 startPos2;
    Vector2 startPos3;
    Vector2 startPos4;

    public Vector2 endPos1;
    public Vector2 endPos2;
    public Vector2 endPos3;
    public Vector2 endPos4;

    public float lerpTime;

    private Animator daAnimator;

    private void Start()
    {
        daAnimator = GetComponent<Animator>();

        startPos1 = image1.transform.position;
        startPos2 = image2.transform.position;
        startPos3 = image3.transform.position;
        startPos4 = image4.transform.position;
    }

    private void Update()
    {
        if (counter == 1)
        {
            //image1.transform.position = Vector2.Lerp(startPos1, endPos1, lerpTime);
        }
        if (counter == 2)
        {
            //move image 2 into position
        }
        if (counter == 3)
        {
            //move image 3 into position
        }
        if (counter == 4)
        {
            //move image 4 into position
        }
    }

    public void NextImageButton()
    {
        counter++;
        print(counter);

        if (counter == 1)
        {
            daAnimator.Play("FirstMission");
        }

        if (counter >= 5)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
