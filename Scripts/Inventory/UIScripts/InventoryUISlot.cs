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
    public InventorySlot SlotData { get; private set; }



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

}

}
