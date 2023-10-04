namespace InventorySystem
{
    
using DisplayMode = InventoryUISlot.DisplayMode;

public class SellTab
{
    /// <summary>
    /// Activates inventory slots that implement IMarketable (we can sell / buy them).
    /// </summary>
    public static void ShowSellSlots(InventoryPanelBuilder inventoryPanel)
    {
        var currentSlots = inventoryPanel.CurrentSlots;
        foreach (var element in currentSlots)
        {
            if (element.Key.Item is IMarketable) 
            {
                var uiSlot = element.Value;
                uiSlot.DiplaySlot(DisplayMode.Price);
                uiSlot.gameObject.SetActive(true);
            }

            else
            {
                var uiSlot = element.Value;
                uiSlot.gameObject.SetActive(false);
            }
        }
    }

}

}
