namespace InventorySystem
{

using SlotState = InventoryUISlot.SlotState;

public class QuestTab : InventoryTab
{
    /// <summary>
    /// Activates quest inventory slots and deactivate other.
    /// </summary>
    public override void OpenTab()
    {
        var playerPanel = UserUiManager.Instance.playerInventory;
        var merchantPanel = UserUiManager.Instance.merchantPanel;

        foreach (var element in playerPanel.CurrentSlots)
        {
            if (element.Key.Item is QuestItem) 
            {
                var uiSlot = element.Value;
                uiSlot.DiplaySlot(SlotState.Default);
                uiSlot.gameObject.SetActive(true);
            }

            else
            {
                var uiSlot = element.Value;
                uiSlot.gameObject.SetActive(false);
            }
        }

        // TODO: for all Tabs make merchant slots non visible more smart way without iteration
        foreach (var uiSlot in merchantPanel.CurrentSlots.Values)
        {
            var go = uiSlot.gameObject; 
            if (go.activeSelf) { go.SetActive(false); }
        }
    }

    public override void CloseTab()
    {
        
    }

}

}
