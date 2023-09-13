namespace Inventory
{

using DisplayMode = InventoryUISlot.DisplayMode;

public class OnBoardTab
{
    /// <summary>
    /// Activates all inventory slots.
    /// </summary>
    public static void ShowOnBoardSlots(InventoryPanelBuilder inventoryPanel)
    {
        var currentSlots = inventoryPanel.CurrentSlots;
        foreach (var element in currentSlots)
        {
            var uiSlot = element.Value;
            if (!uiSlot.gameObject.activeSelf)  { uiSlot.gameObject.SetActive(true); }
            uiSlot.DiplaySlot(DisplayMode.Default);
        }
    }
}

}
