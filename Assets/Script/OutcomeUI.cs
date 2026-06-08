using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutcomeUI : MonoBehaviour, IDragHandler
{
    [Header("Root")]
    [SerializeField] private GameObject popupRoot;
    [SerializeField] Canvas canvas;
    [SerializeField] private TMP_Text titleText;

    [Header("Mode Areas")]
    [SerializeField] private GameObject qrArea;
    [SerializeField] private GameObject galleryArea;

    [Header("QR")]
    [SerializeField] private Image qrImage;

    [Header("Gallery")]
    [SerializeField] private Image galleryImage;

    [Header("Buttons")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;

    [Header("M1")]
    [SerializeField] private Sprite[] m1Images;

    [Header("M3")]
    [SerializeField] private Sprite[] m3Images;

    [Header("QR")]
    [SerializeField] private Sprite[] QRImages;


    private Sprite[] currentImages;
    private int currentIndex;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = popupRoot.GetComponent<RectTransform>();

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        prevButton.onClick.AddListener(ShowPrevImage);
        nextButton.onClick.AddListener(ShowNextImage);
        closeButton.onClick.AddListener(ClosePopup);

        ClosePopup();
    }

    public void ShowImageOutcome(string popupTitle,  string moduleId)
    {
        Debug.Log("ShowImageOutcome called");
        Debug.Log("popupRoot: " + popupRoot.name);

        popupRoot.SetActive(true);
        
        qrArea.SetActive(false);
        galleryArea.SetActive(true);

        
        titleText.text = popupTitle;

        switch (moduleId)
        {
            case "M1":
                currentImages = m1Images;
                break;
            case "M3":
                currentImages = m3Images;
                break;
            default:
                Debug.LogError("No gallery mapped for moduleId: " + moduleId);
                return;
        }

        currentIndex = 0;

        prevButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);

        RefreshGallery();
    }

    public void ShowQROutcome(string popupTitle,  string moduleId)
    {
        Debug.Log("ShowQROutcome called");
        Debug.Log("popupRoot: " + popupRoot.name);

        Sprite image = GetQrByModuleId(moduleId);

        popupRoot.SetActive(true);
        qrArea.SetActive(true);
        galleryArea.SetActive(false);

        titleText.text = popupTitle;
        currentImages = null;
        currentIndex = 0;

        prevButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        qrImage.sprite = image;
        qrImage.preserveAspect = true;
    }
    public void ClosePopup()
    {
        popupRoot.SetActive(false);
        qrArea.SetActive(false);
        galleryArea.SetActive(false);
    }

    public void RefreshGallery()
    {
        if (currentImages == null || currentImages.Length == 0)
        {
            Debug.LogError("currentImages is null or empty");
            return;
        }

        Sprite sprite = currentImages[currentIndex];
        galleryImage.sprite = sprite;
        galleryImage.preserveAspect = true;
    }

    public void ShowNextImage()
    {
        currentIndex = (currentIndex + 1) %currentImages.Length;
        RefreshGallery();
    }

    public void ShowPrevImage()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = currentImages.Length - 1;

        RefreshGallery();
    }

    private Sprite GetQrByModuleId(string moduleId)
    {
        moduleId = moduleId.ToUpper();

        switch (moduleId)
        {
            case "M2":
                return QRImages[0];
            case "M4":
                return QRImages[1];
            case "M5":
                return QRImages[2];
            case "M7":
                return QRImages[3];
            default:
                Debug.LogError("No QR mapped for moduleId: " + moduleId);
                return null;
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
