using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{

public class InventoryUISlotPanel : MonoBehaviour, IDeselectHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private TMP_Text descriptionText;


    #region Hide on click outside of panel

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);    
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Destroy(gameObject);
    }

    #endregion


    public void BuildPanel(InventoryItem item)
    {
        icon.sprite = item.BigIcon;
        nameText.text = item.Name;
        descriptionText.text = item.Description;

        if (item.Weight != 0)
        {
            weightText.text = $"{item.Weight}";
        }
        else
        {
            weightText.gameObject.SetActive(false);
        }

        if (item is IMarketable marketable)
        { 
            priceText.text = $"{marketable.RegPrice}";
        }
        else 
        {
            priceText.gameObject.SetActive(false);
        }
    }
}

}
