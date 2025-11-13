using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlightedObject;
    private RaycastHit hitInfo;
    public float MaxRaycastDistance = 5f;
    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
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
                    print("Hello");
                }

                outline.enabled = true;
                highlightedObject = hitTransform;

                // Press E to collect and destroy
                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.ScrapMetal += 1;
                    Destroy(hitTransform.gameObject);
                    highlightedObject = null; // clear reference
                }
            }
        }
    }
}
