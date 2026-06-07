using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [Header("Image")]
    [SerializeField] private Image thumb;
    [SerializeField] private Sprite[] thumbImages;

    private float thumbMaxWidth;
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
        thumbMaxWidth = thumb.rectTransform.sizeDelta.x;
        
    }

    // update UI
    public void Show(ModuleData data)
    {
        currentData = data;
        root.SetActive(true);

        smallTitle.text = data.iconId + " " + data.iconTitle;
        mainTitle.text = data.title;

        filePath.text = data.path;
        completed.text = data.completed;

        module.text = "module "+data.moduleNumber;
        challenge.text = data.focus;
        whatIDid.text = data.whatIDid;
        keyResult.text = data.keyResult;
        learned.text = data.whatILearned;

        tag1.text = data.tags[0];
        tag2.text = data.tags[1];
        tag3.text = data.tags[2];

        for(int i = 0;  i < thumbImages.Length; i++)
        {
            if(thumbImages[i].name.StartsWith(data.thumbnailKey))
            {
                Sprite image = thumbImages[i];
                thumb.sprite = image;

                float ratio = image.rect.height / image.rect.width;
                Debug.Log("thumb max: " + thumbMaxWidth);
                float newWidth = thumbMaxWidth;
                float newHeight = newWidth * ratio;

                thumb.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

                return;
            }
        }

        play.gameObject.SetActive(data.canPlay);
    }
    public void Hide()
    {
        root.SetActive(false);
    }

    public void OnClickOutcome()
    {
        Debug.Log("outcome button clicked");
    }

    public void OnClickPlay()
    {
        Debug.Log("play button clicked");
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
