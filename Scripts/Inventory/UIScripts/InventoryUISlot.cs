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

    [SerializeField] private RarityColors rarityColors;



    public void BuildSlot(InventorySlot itemData)
    {
        itemIcon.sprite = itemData.Item.SmallIcon;
        amount.text = $"{itemData.Amount}";

        bg.color = rarityColors.GetColorRarity(itemData.Item.Rarity);
    }

}

}
