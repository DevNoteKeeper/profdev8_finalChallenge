using UnityEngine;
using UnityEngine.EventSystems;

public class PopupController : MonoBehaviour, IDragHandler
{
    public GameObject popup;
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }
    public void CloseButton()
    {
        popup.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null)
        {
            return;
        }
        rectTransform.anchoredPosition
             += eventData.delta / canvas.scaleFactor;
    }
}
