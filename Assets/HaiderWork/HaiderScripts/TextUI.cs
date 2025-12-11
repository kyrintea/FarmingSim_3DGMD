using UnityEngine;

public class TextUI : MonoBehaviour
{
    public GameObject CraftingStarted;
    public GameObject NotEnoughScraps;
    public GameObject UseDoor;
    public GameObject MetalCollected;
    public GameObject EatFood;
    public GameObject CraftBatteries;
    public GameObject CollectScraps;
    public GameObject TextE;
    public GameObject InventoryFull;

    
    void Start()
    {
        CraftingStarted.SetActive(false);
        NotEnoughScraps.SetActive(false);
        UseDoor.SetActive(false);
        MetalCollected.SetActive(false);
        EatFood.SetActive(false);
        CraftBatteries.SetActive(false);
        CollectScraps.SetActive(false);
        InventoryFull.SetActive(false);
    }

    public void craftingStarted()
    {
        CraftingStarted.SetActive(true);
        
        NotEnoughScraps.SetActive(false);
        UseDoor.SetActive(false);
        MetalCollected.SetActive(false);
        EatFood.SetActive(false);
        CraftBatteries.SetActive(false);
        CollectScraps.SetActive(false);
        InventoryFull.SetActive(false);
    }

    public void notEnoughScraps()
    {
        NotEnoughScraps.SetActive(true);
        
        CraftingStarted.SetActive(false);
        UseDoor.SetActive(false);
        MetalCollected.SetActive(false);
        EatFood.SetActive(false);
        CraftBatteries.SetActive(false);
        CollectScraps.SetActive(false);
        InventoryFull.SetActive(false);
    }

    public void craftBatteries()
    {
        CraftBatteries.SetActive(true);

        CraftingStarted.SetActive(false);
        NotEnoughScraps.SetActive(false);
        UseDoor.SetActive(false);
        MetalCollected.SetActive(false);
        EatFood.SetActive(false);
        CollectScraps.SetActive(false);
        InventoryFull.SetActive(false);
    }
}
