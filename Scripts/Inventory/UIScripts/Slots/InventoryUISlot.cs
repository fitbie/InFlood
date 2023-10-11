using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace InventorySystem
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

    public enum SlotState { Default, Price }
    public SlotState CurrentDisplayMode { get; private set; }



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
    public void DiplaySlot(SlotState displayMode)
    {
        switch (displayMode)
        {
            case SlotState.Default:
            ShowDefaultSlot();
            break;

            case SlotState.Price:
            ShowPriceSlot();
            break;
        }
    }


    public void ShowDefaultSlot()
    {
        GameObject priceGO = price.gameObject;
        if (priceGO.activeSelf) priceGO.SetActive(false);

        CurrentDisplayMode = SlotState.Default;
    }


    public void ShowPriceSlot()
    {
        var marketable = SlotData.Item as IMarketable;
        price.text = $"{marketable.RegPrice}";
        price.gameObject.SetActive(true);

        CurrentDisplayMode = SlotState.Price;
    }

}

}
