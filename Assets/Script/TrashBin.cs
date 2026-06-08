using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler, IDragHandler
{
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
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DesktopIcon icon = eventData.pointerDrag.GetComponent<DesktopIcon>();
        if (icon != null)
        {
            icon.HideIcon();
        }
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
