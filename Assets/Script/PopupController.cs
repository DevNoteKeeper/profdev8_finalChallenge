using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popup;
    public void CloseButton()
    {
        popup.SetActive(false);
    }
}
