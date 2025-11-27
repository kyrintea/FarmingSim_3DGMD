using UnityEngine;
using System.Collections;

public class CraftingBench : MonoBehaviour
{
    private Player player;
    private bool isPlayerNearby = false;
    private bool CanCraft = true;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (isPlayerNearby == true && Input.GetKeyDown(KeyCode.E) && CanCraft == true)
        {
            if (player.ScrapMetal >= 5)
            {
                StartCoroutine(WaitAndPrint(5f));
            }
            else
            {
                print("Not enough scrap!");
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

    IEnumerator WaitAndPrint(float waitTime)
    {
        print("Crafting started...");
        player.ScrapMetal -= 5;
        CanCraft = false;
        yield return new WaitForSeconds(waitTime);
        print("Craft SUCCESS!");
        CanCraft = true;
    }
}
