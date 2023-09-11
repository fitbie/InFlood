using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace Inventory
{

public class InventoryPanelBuilder : MonoBehaviour
{
    [SerializeField] private Inventory inventory; // TODO : setup accessing to.
    [SerializeField] private InventoryUISlot slotPrefab;
    [SerializeField] private List<InventoryUISlot> currentSlots = new List<InventoryUISlot>();



    private void Start() // TODO entry point.
    {
        Initialize();    
    }


    private void Initialize()
    {
        BuildPanel();

        inventory.ItemAmountAdded += UpdateUISlot;
        inventory.ItemAmountRemoved += UpdateUISlot;
    }


    public void BuildPanel()
    {
        Profiler.BeginSample("BuildPanel");
        var inventorySlots = GetSlots();
        
        var sortedSlots = SortSlotsByType(inventorySlots);

        foreach(var inventorySlot in sortedSlots)
        {
            BuildUISlot(inventorySlot);
        }
        Profiler.EndSample();
    }


    protected virtual List<InventorySlot> GetSlots()
    {
        List<InventorySlot> slots = inventory.InventorySlots;
        return slots;
    }


    private void BuildUISlot(InventorySlot inventorySlot) // Very first building
    {
        var slot = Instantiate(slotPrefab, transform);
        slot.BuildSlot(inventorySlot);
        currentSlots.Add(slot);
    }


    /// <summary>
    /// Called when we modify inventory by adding / removing items. Avoid building / destroying slots every time. (GC optimization)
    /// </summary>
    private void UpdateUISlot(InventorySlot inventorySlot, int _)
    {
        Profiler.BeginSample("UpdateUISlot");
        // Update if we have current item ui slot.
        foreach (var uiSlot in currentSlots)
        {
            if (inventorySlot.Item == uiSlot.SlotData.Item) 
            { 
                uiSlot.UpdateSlot();
                Profiler.EndSample();
                return;
            }    
        }
        
        BuildUISlot(inventorySlot); // Else - build new.
        Profiler.EndSample();
    }




        #region TypeSorting

        private List<Type> typeOrder = new List<Type>
    {
        typeof(FoodItem),
        typeof(FuelItem),
        typeof(ScienceItem),
    };


    private List<InventorySlot> SortSlotsByType(List<InventorySlot> inventorySlots)
    {
        var groupedSlots = inventorySlots.GroupBy(slot => slot.Item.GetType());
        var sortedSlots = groupedSlots.OrderBy(group => typeOrder.IndexOf(group.Key))
        .SelectMany(group => group)
        .ToList();

        return sortedSlots;
    }

    #endregion

}

}
