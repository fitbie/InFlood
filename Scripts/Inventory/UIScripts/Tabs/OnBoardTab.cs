namespace InventorySystem
{

using SlotState = InventoryUISlot.SlotState;

public class OnBoardTab : InventoryTab
{
    /// <summary>
    /// Activates all inventory slots.
    /// </summary>
    public override void OpenTab()
    {
        var playerPanel = UserUiManager.Instance.playerInventory;
        var merchantPanel = UserUiManager.Instance.merchantPanel;

        foreach (var element in playerPanel.CurrentSlots)
        {
            var uiSlot = element.Value;
            if (!uiSlot.gameObject.activeSelf)  { uiSlot.gameObject.SetActive(true); }
            uiSlot.DiplaySlot(SlotState.Default);
        }

        // TODO: for all Tabs make merchant slots non visible more smart way without iteration
        foreach (var uiSlot in merchantPanel.CurrentSlots.Values)
        {
            var go = uiSlot.gameObject; 
            if (go.activeSelf) { go.SetActive(false); }
        }
    }


    public override void CloseTab( )
    {
        
    }

}

}
