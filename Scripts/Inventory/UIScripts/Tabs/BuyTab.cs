namespace InventorySystem
{

using SlotState = InventoryUISlot.SlotState;

public class BuyTab : InventoryTab
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


    public override void OpenTab()
    {
        var playerPanel = UserUiManager.Instance.playerInventory;
        var merchantPanel = UserUiManager.Instance.merchantPanel;

        foreach (var uiSlot in merchantPanel.CurrentSlots.Values)
        {
            uiSlot.DiplaySlot(SlotState.Price);
            uiSlot.gameObject.SetActive(true);
        }

        foreach (var uiSlot in playerPanel.CurrentSlots.Values)
        {
            var go = uiSlot.gameObject;
            if (go.activeSelf) { go.SetActive(false); }
        }
    }


    public override void CloseTab() {}

}

}