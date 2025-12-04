using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CraftingBench : MonoBehaviour
{
    private Player player;
    private bool isPlayerNearby = false;
    private bool CanCraft = true;
    private TextUI textUI;
    public bool IsCrafting = false;

    void Start()
    {
        //Get Scripts
        player = FindAnyObjectByType<Player>();
        textUI = FindAnyObjectByType<TextUI>();
    }

    void Update()
    {
        if (IsCrafting)
        {
            textUI.CraftBatteries.SetActive(false);
            return;
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && CanCraft)
        {
            if (player.ScrapMetal >= 5)
            {
                StartCoroutine(WaitAndPrint(5f, 5f));
            }
            else
            {
                StartCoroutine(UIWaitAndPrint(textUI.NotEnoughScraps, 3f));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            textUI.CraftBatteries.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            textUI.CraftBatteries.SetActive(false);
        }
    }

    IEnumerator WaitAndPrint(float UITextTime, float waitTime)
    {
        print("Crafting started...");
        IsCrafting = true;
        textUI.craftingStarted();
        player.ScrapMetal -= 5;
        CanCraft = false;
        yield return new WaitForSeconds(UITextTime);
        textUI.CraftingStarted.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        print("Craft SUCCESS!");
        IsCrafting = false;
        CanCraft = true;
    }

    IEnumerator UIWaitAndPrint(GameObject UIText, float waitTime)
    {
        UIText.SetActive(true);
        textUI.notEnoughScraps();
        print("Not enough scrap!");
        yield return new WaitForSeconds(waitTime);
        UIText.SetActive(false);
    }
}
