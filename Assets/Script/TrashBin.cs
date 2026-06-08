using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DesktopIcon icon = eventData.pointerDrag.GetComponent<DesktopIcon>();
        if (icon != null)
        {
            icon.HideIcon();
        }
    }
}
