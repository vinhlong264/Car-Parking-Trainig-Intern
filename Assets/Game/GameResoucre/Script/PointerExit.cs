using UnityEngine;
using UnityEngine.EventSystems;

public class PointerExit : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject ButtonSetting;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            ButtonSetting.SetActive(true);
        }
    }
}
