using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlightedObject;
    private RaycastHit hitInfo;
    public float MaxRaycastDistance = 3f;
    private Player player;
    private BARSmanagerScript bARSmanagerScript;
    private Spaceship spaceship;
    private TextUI textUI;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        bARSmanagerScript = FindAnyObjectByType<BARSmanagerScript>();
        spaceship = FindAnyObjectByType<Spaceship>();
        textUI = FindAnyObjectByType<TextUI>();
    }

    void Update()
    {
        // Turn off previous highlight
        if (highlightedObject != null)
        {
            Outline previousOutline = highlightedObject.GetComponent<Outline>();
            
            textUI.TextE.SetActive(false);
            textUI.UseDoor.SetActive(false);
            textUI.EatFood.SetActive(false);
            textUI.CraftBatteries.SetActive(false);
            textUI.CollectScraps.SetActive(false);

            if (previousOutline != null)
                previousOutline.enabled = false;

            highlightedObject = null;
        }

        // Cast ray from camera to mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hitInfo, MaxRaycastDistance))
        {
            Transform hitTransform = hitInfo.transform;

            // Only react to objects on "OutLine" layer
            if (hitTransform.gameObject.layer == LayerMask.NameToLayer("OutLine"))
            {
                // Enable outline
                Outline outline = hitTransform.GetComponent<Outline>();
                
                if (outline == null)
                {
                    outline = hitTransform.gameObject.AddComponent<Outline>();
                    outline.OutlineColor = Color.white; // highlight color
                    outline.OutlineWidth = 20f;
                }

                outline.enabled = true;
                textUI.TextE.SetActive(true);
                highlightedObject = hitTransform;

                // Press E to collect and destroy
                if (hitTransform.gameObject.CompareTag("Metal"))
                {
                    textUI.CollectScraps.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        player.ScrapMetal += 1;
                        Destroy(hitTransform.gameObject);

                        textUI.TextE.SetActive(false);
                        textUI.CollectScraps.SetActive(true);
                        highlightedObject = null;

                        return;   // <- prevents highlight from reactivating   
                    }
                }
                else if (hitTransform.gameObject.CompareTag("Heal"))
                {
                    textUI.EatFood.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        bARSmanagerScript.Heal(25f);

                        textUI.TextE.SetActive(false);
                        highlightedObject = null;

                        return;   
                    }
                }
                else if (hitTransform.gameObject.CompareTag("ShipDoor"))
                {
                    textUI.UseDoor.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        spaceship.ToggleDoor();

                        textUI.TextE.SetActive(false);
                        highlightedObject = null;

                        return;   
                    }
                }
                else if (hitTransform.gameObject.CompareTag("CraftingBench"))
                {
                    textUI.craftBatteries();
                }
            }
        }
    }
}
