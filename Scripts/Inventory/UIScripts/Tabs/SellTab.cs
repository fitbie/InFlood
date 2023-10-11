namespace InventorySystem
{
    using SlotState = InventoryUISlot.SlotState;

public class SellTab : InventoryTab
{
    protected override void Start()
    {
        base.Start();

        if (Merchant.Current != null) { TabState(Merchant.Current); } // TODO: Initialization ?

        Merchant.OnMerchantTriggered += TabState;
    }


    private void TabState(Merchant merchant)
    {
        button.interactable = merchant.IsTriggered;
    }  


    /// <summary>
    /// Activates inventory slots that implement IMarketable (we can sell / buy them).
    /// </summary>
    public override void OpenTab()
    {
        var playerPanel = UserUiManager.Instance.playerInventory;
        var merchantPanel = UserUiManager.Instance.merchantPanel;

        foreach (var element in playerPanel.CurrentSlots)
        {
            if (element.Key.Item is IMarketable) 
            {
                var uiSlot = element.Value;
                uiSlot.DiplaySlot(SlotState.Price);
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


    public override void CloseTab() {}

}

}
