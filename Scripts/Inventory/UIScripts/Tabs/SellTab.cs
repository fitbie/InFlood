using UnityEngine;

namespace Inventory
{

public class SellTab : MonoBehaviour
{
    [SerializeField] private InventoryPanelBuilder inventoryPanelBuilder;

    
    public void OpenSellTab()
    {
        var currentSlots = inventoryPanelBuilder.CurrentSlots;
        foreach (var element in currentSlots)
        {
            if (element.Key.Item is IMarketable marketable) 
            {
                var uiSlot = element.Value;
                uiSlot.ShowPrice(true);
                uiSlot.gameObject.SetActive(true);
            }

            else
            {
                var uiSlot = element.Value;
                uiSlot.ShowPrice(false);
                uiSlot.gameObject.SetActive(false);
            }
        }
    }

}

}
