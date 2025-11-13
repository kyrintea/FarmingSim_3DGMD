using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float sizeDifference = 1.05f;
    public float wiggleSpeed = 2f;
    public float wiggleMagnitude = 5f;
    private Vector2 originalSize;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = originalSize * sizeDifference;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = originalSize;

    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
