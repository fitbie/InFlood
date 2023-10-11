namespace InventorySystem
{

public class ClickOnMerchantInventorySlotHandler : ClickOnInventorySlotHandler
{
    protected override void PriceInteract()
    {
        Merchant.Current.BuyItem(uiSlot.SlotData);
    }

}

}
