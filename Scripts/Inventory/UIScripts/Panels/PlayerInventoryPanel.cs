using UnityEngine;

namespace InventorySystem
{
[AddComponentMenu("Inventory/Player Inventory Panel")]
public class PlayerInventoryPanel : InventoryPanelController
{
    public override void Initialize()
    {
        Inventory = GameManager.Instance.Player.Inventory;

        base.Initialize();
    }


    protected override void AddUISlot(InventorySlot slotData, Transform container, out InventoryUISlot addedSlot)
    {
        base.AddUISlot(slotData, container, out addedSlot);
        
        InventoryUISlotsSorter.SortChildrenByItemType(CurrentSlots);
    }


    protected override void RemoveUISlot(InventorySlot inventorySlot)
    {
        base.RemoveUISlot(inventorySlot);

        InventoryUISlotsSorter.SortChildrenByItemType(CurrentSlots);
    }

}

}
