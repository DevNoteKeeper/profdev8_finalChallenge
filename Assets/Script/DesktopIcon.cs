using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] TMP_Text iconText;
    [SerializeField] Canvas canvas;
    [SerializeField] DesktopManager manager;

    private RectTransform rectTransform;
    private Image image;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private bool isDragging;
    
    private static DesktopIcon currentSelectedIcon;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        if(canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        SetAlpha(0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDragging) return;

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
                string title = iconText.text;
                manager.OnIconClicked(title);
            }
            else
            {
                Debug.Log("no Text");
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        startPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
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
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        isDragging=false;
    }

    public void HideIcon()
    {
        gameObject.SetActive(false);
    }
    public void ResetToStartPosition()
    {
        rectTransform.anchoredPosition = startPosition;
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
