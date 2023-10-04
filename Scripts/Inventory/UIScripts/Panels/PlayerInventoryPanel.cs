namespace InventorySystem
{

public class PlayerInventoryPanel : InventoryPanelBuilder
{
    public InventoryTabsController tabsController;


    public override void Initialize()
    {
        Inventory = GameManager.Instance.Player.Inventory;

        tabsController.InventoryPanel = this;

        base.Initialize();
    }


        protected override void BuildUISlot(InventorySlot inventorySlot) // Very first building
    {
        base.BuildUISlot(inventorySlot);
        
        InventoryUISlotsSorter.SortChildrenByItemType(CurrentSlots);
    }


    protected override void RemoveUISlot(InventorySlot inventorySlot)
    {
        base.RemoveUISlot(inventorySlot);

        InventoryUISlotsSorter.SortChildrenByItemType(CurrentSlots);
    }

}

}
