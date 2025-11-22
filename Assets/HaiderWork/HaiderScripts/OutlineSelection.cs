using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlightedObject;
    private RaycastHit hitInfo;
    public float MaxRaycastDistance = 5f;
    private Player player;
    private BARSmanagerScript bARSmanagerScript;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        bARSmanagerScript = FindAnyObjectByType<BARSmanagerScript>();
    }

    void Update()
    {
        // Turn off previous highlight
        if (highlightedObject != null)
        {
            Outline previousOutline = highlightedObject.GetComponent<Outline>();
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
                    outline.OutlineColor = Color.magenta; // highlight color
                    outline.OutlineWidth = 20f;
                }

                outline.enabled = true;
                highlightedObject = hitTransform;

                // Press E to collect and destroy
                if (hitTransform.gameObject.CompareTag("Metal") && Input.GetKeyDown(KeyCode.E))
                {
                    player.ScrapMetal += 1;
                    Destroy(hitTransform.gameObject);
                    highlightedObject = null; // clear reference
                }
                else if (hitTransform.gameObject.CompareTag("Heal") && Input.GetKeyDown(KeyCode.E))
                {
                    bARSmanagerScript.Heal(25f);
                }
            }
        }
    }
}
