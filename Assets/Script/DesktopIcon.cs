using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] TMP_Text iconText;
    [SerializeField] Canvas canvas;
    private RectTransform rectTransform;
    private Image image;

    private static DesktopIcon currentSelectedIcon;

    public string getIconText => iconText.text;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        if(canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        SetAlpha(0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        if(eventData.clickCount == 1)
        {
            if(currentSelectedIcon != null && currentSelectedIcon != this)
            {
                currentSelectedIcon.SetAlpha(0f);
            }
            SetAlpha(0.5f);
            currentSelectedIcon = this;
        }
        if(eventData.clickCount == 2)
        {
            if(iconText != null)
            {
                Debug.Log(iconText.text);
            }
            else
            {
                Debug.Log("no Text");
            }
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
    private void SetAlpha(float alpha)
    {
        if (image != null) { 
        Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
