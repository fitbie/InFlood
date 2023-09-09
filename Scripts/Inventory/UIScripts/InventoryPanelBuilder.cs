using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{

public class InventoryPanelBuilder : MonoBehaviour
{
    [SerializeField] private Inventory inventory; // TODO : setup accessing to.
    [SerializeField] private InventoryUISlot slotPrefab;

    List<Type> typeOrder = new List<Type>
    {
        typeof(FoodItem),
        typeof(FuelItem),
        typeof(ScienceItem),
    };


    public void BuildPanel()
    {
        List<InventorySlot> inventorySlots = inventory.InventorySlots;
        
        var groupedSlots = inventorySlots.GroupBy(slot => slot.Item.GetType());
        var sortedSlots = groupedSlots.OrderBy(group => typeOrder.IndexOf(group.Key))
        .SelectMany(group => group)
        .ToList();

        foreach(var inventorySlot in sortedSlots)
        {
            var slot = GameObject.Instantiate(slotPrefab, transform);
            slot.BuildSlot(inventorySlot);
        }
    }

}

}
