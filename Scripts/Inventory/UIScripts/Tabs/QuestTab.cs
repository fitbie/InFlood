namespace InventorySystem
{

using DisplayMode = InventoryUISlot.DisplayMode;

public class QuestTab
{
    /// <summary>
    /// Activates quest inventory slots and deactivate other.
    /// </summary>
    public static void ShowQuestSlots(InventoryPanelBuilder inventoryPanel)
    {
        var currentSlots = inventoryPanel.CurrentSlots;
        foreach (var element in currentSlots)
        {
            if (element.Key.Item is QuestItem) 
            {
                var uiSlot = element.Value;
                uiSlot.DiplaySlot(DisplayMode.Default);
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
