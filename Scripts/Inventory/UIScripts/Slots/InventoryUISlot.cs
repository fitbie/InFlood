using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Inventory
{

/// <summary>
/// Controls the displaying of UI inventory slot.
/// </summary>
public class InventoryUISlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon; // Item image.
    [SerializeField] private Image bg; // Image behind item.
    [SerializeField] private TMP_Text amount;
    [SerializeField] private TMP_Text price;
    public InventorySlot SlotData { get; private set; }

    public enum DisplayMode { Default, Price }
    private DisplayMode mode;



    public virtual void BuildSlot(InventorySlot itemData)
    {
        SlotData = itemData;

        itemIcon.sprite = SlotData.Item.SmallIcon;
        amount.text = $"{SlotData.Amount}";

        bg.color = SlotData.Item.TypeColor;
    }


    public virtual void UpdateSlot()
    {
        itemIcon.sprite = SlotData.Item.SmallIcon;
        amount.text = $"{SlotData.Amount}";

        bg.color = SlotData.Item.TypeColor;  
    }


    /// <summary>
    /// How slot should be displayed - default, with price text, etc..
    /// </summary>
    public void DiplaySlot(DisplayMode displayMode)
    {
        switch (displayMode)
        {
            case DisplayMode.Default:
            ShowDefaultSlot();
            break;

            case DisplayMode.Price:
            ShowPriceSlot();
            break;
        }
    }


    public void ShowDefaultSlot()
    {
        GameObject priceGO = price.gameObject;
        if (priceGO.activeSelf) priceGO.SetActive(false);
    }


    public void ShowPriceSlot()
    {
        var marketable = SlotData.Item as IMarketable;
        price.text = $"{marketable.RegPrice}";
        price.gameObject.SetActive(true);
    }

}

}
