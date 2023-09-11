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
    [SerializeField] private Transform content; // Parent of slots with grid layout group.

    private Dictionary<InventorySlot, InventoryUISlot> currentSlots = new Dictionary<InventorySlot, InventoryUISlot>();



    private void Start() // TODO entry point.
    {
        Initialize();    
    }


    private void Initialize()
    {
        BuildPanel();

        inventory.ItemAmountAdded += UpdateUISlot;
        inventory.ItemAmountRemoved += UpdateUISlot;
        inventory.ItemDestroyed += RemoveUISlot;
    }


    public void BuildPanel()
    {
        var inventorySlots = GetSlots();

        foreach(var inventorySlot in inventorySlots)
        {
            BuildUISlot(inventorySlot);
        }
    }


    protected virtual List<InventorySlot> GetSlots()
    {
        List<InventorySlot> slots = inventory.InventorySlots;
        return slots;
    }


    /// <summary>
    /// Called when we modify inventory by adding / removing items. Avoid building / destroying slots every time. (GC optimization)
    /// </summary>
    private void UpdateUISlot(InventorySlot inventorySlot, int _)
    {        
        // Update if we have current item ui slot.
        if (currentSlots.TryGetValue(inventorySlot, out var uiSlot))
        {
            uiSlot.UpdateSlot();
        }

        // Build new slot.
        else 
        {
            BuildUISlot(inventorySlot);    
        }
    }


    private void BuildUISlot(InventorySlot inventorySlot) // Very first building
    {
        var slot = Instantiate(slotPrefab, content);
        slot.BuildSlot(inventorySlot);
        currentSlots.Add(inventorySlot, slot);
        
        SortChildrenByItemType();
    }


    private void RemoveUISlot(InventorySlot inventorySlot)
    {
        currentSlots.Remove(inventorySlot, out var uiSlot);
        Destroy(uiSlot.gameObject);

        SortChildrenByItemType();
    }


    #region TypeSorting

    // Determines in which order the sorting will proceed. If we add new item typed - update this list.
    private List<Type> typeOrder = new List<Type>
    {
        typeof(FoodItem),
        typeof(FuelItem),
        typeof(ScienceItem),
    };


    public void SortChildrenByItemType()
    {
        // Get all keys(InventorySlot) and order them using typeOrder.
        var sortedKeys = currentSlots.Keys.OrderBy(key => typeOrder.IndexOf(key.Item.GetType()));

        // Move slots position accordong to sordedKeys
        foreach (var key in sortedKeys)
        {
            var uiSlot = currentSlots[key];
            uiSlot.transform.SetAsLastSibling();
        }
    }

    #endregion
}

}
