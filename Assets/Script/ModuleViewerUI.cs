using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ModuleViewerUI : MonoBehaviour, IDragHandler
{
    [Header("Root")]
    [SerializeField] private GameObject root;
    [SerializeField] Canvas canvas;

    [Header("Text")]
    [SerializeField] private TMP_Text module;
    [SerializeField] private TMP_Text smallTitle;
    [SerializeField] private TMP_Text mainTitle;
    [SerializeField] private TMP_Text filePath;
    [SerializeField] private TMP_Text completed;
    [SerializeField] private TMP_Text challenge;
    [SerializeField] private TMP_Text whatIDid;
    [SerializeField] private TMP_Text keyResult;
    [SerializeField] private TMP_Text learned;

    [Header("Tag")]
    [SerializeField] private TMP_Text tag1;
    [SerializeField] private TMP_Text tag2;
    [SerializeField] private TMP_Text tag3;

    [Header("Button")]
    [SerializeField] private Button outcome;
    [SerializeField] private Button play;
    [SerializeField] private OutcomeUI outcomeUI;

    [Header("Image")]
    [SerializeField] private Image thumb;
    [SerializeField] private Sprite[] thumbImages;

    private float thumbMaxWidth;
    private float thumbMaxHeight;

    private ModuleData currentData;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
        if(thumb == null)
        {
            thumb = GetComponentInChildren<Image>();
        }

        thumbMaxWidth = thumb.rectTransform.sizeDelta.x -20;
        thumbMaxHeight = thumb.rectTransform.sizeDelta.y-20;

    }

    // update UI
    public void Show(ModuleData data)
    {
        currentData = data;
        root.SetActive(true);

        smallTitle.text = currentData.iconId + " " + currentData.iconTitle;
        mainTitle.text = currentData.title;

        filePath.text = currentData.path;
        completed.text = currentData.completed;

        module.text = "module " + currentData.moduleNumber;
        challenge.text = currentData.focus;
        whatIDid.text = currentData.whatIDid;
        keyResult.text = currentData.keyResult;
        learned.text = currentData.whatILearned;

        tag1.text = currentData.tags[0];
        tag2.text = currentData.tags[1];
        tag3.text = currentData.tags[2];

        play.gameObject.SetActive(currentData.canPlay);

        for (int i = 0; i < thumbImages.Length; i++)
        {
            if (thumbImages[i].name.StartsWith(currentData.thumbnailKey))
            {
                Sprite image = thumbImages[i];
                thumb.sprite = image;

                float imageWidth = image.rect.width;
                float imageHeight = image.rect.height;

                float scaleX = thumbMaxWidth / imageWidth;
                float scaleY = thumbMaxHeight / imageHeight;
                float scale = Mathf.Min(scaleX, scaleY);

                float newWidth = imageWidth * scale;
                float newHeight = imageHeight * scale;

                thumb.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
                break;
            }
        }

        if (currentData.id == "M6")
        {
            outcome.gameObject.SetActive(false);
        }
        else
        {
            outcome.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnClickOutcome()
    {
        Debug.Log("Outcome button clicked");

        if (currentData == null)
        {
            Debug.LogError("currentData is NULL");
            return;
        }

        Debug.Log("currentData title: " + currentData.title);
        Debug.Log("currentData type: " + currentData.outcomeType);

        if (outcomeUI == null)
        {
            Debug.LogError("outcomeUI is NULL");
            return;
        }

        switch (currentData.outcomeType)
        {
            case OutcomeType.Url:
                outcomeUI.ShowQROutcome(currentData.outcomeTitle,  currentData.id);
                break;

            case OutcomeType.ImageGallery:
                outcomeUI.ShowImageOutcome(currentData.outcomeTitle, currentData.id);
                break;
        }
    }

    //public void OnClickPlay()
    //{
    //    Debug.Log("play button clicked");
    //}

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
