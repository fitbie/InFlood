namespace Inventory
{

public class PlayerInventoryPanel : InventoryPanelBuilder
{

    private void Start() // TODO entry point.
    {
        Initialize();    
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
