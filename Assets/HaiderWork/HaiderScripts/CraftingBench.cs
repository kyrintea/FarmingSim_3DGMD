using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftingBench : MonoBehaviour
{
    private Player player;
    private bool isPlayerNearby = false;
    private bool CanCraft = true;
    private TextUI textUI;

    void Start()
    {
        //Get Scripts
        player = FindAnyObjectByType<Player>();
        textUI = FindAnyObjectByType<TextUI>();
    }

    void Update()
    {
        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.E) && CanCraft == true)
        {
            if (player.ScrapMetal >= 5)
            {
                //The first # is for how long the UI text stays on screen and the 2nd # is how long it takes to craft the item
                //5 + 55 = 60 - 60 seconds to craft item
                StartCoroutine(WaitAndPrint(5f, 5f));
            }
            else
            {
                //The 1st one grabs the gameobject from a different script and the # is how long you wait
                //before the action is done
                StartCoroutine(UIWaitAndPrint(textUI.NotEnoughScraps, 3f));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    IEnumerator WaitAndPrint(float UITextTime, float waitTime)
    {
        textUI.craftingStarted();
        print("Crafting started...");
        player.ScrapMetal -= 5;
        CanCraft = false;
        yield return new WaitForSeconds(UITextTime);
        textUI.CraftingStarted.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        print("Craft SUCCESS!");
        CanCraft = true;
    }

    IEnumerator UIWaitAndPrint(GameObject UIText, float waitTime)
    {
        UIText.SetActive(true);
        print("Not enough scrap!");
        yield return new WaitForSeconds(waitTime);
        UIText.SetActive(false);
    }
}
