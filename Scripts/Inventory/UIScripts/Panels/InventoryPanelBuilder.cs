using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

/// <summary>
/// Base UI inventory panel class to derive from.
/// </summary>
public class InventoryPanelBuilder : MonoBehaviour
{
    [field:SerializeField] public Inventory Inventory { get; protected set; }
    
    [field:SerializeField] public InventoryUISlot SlotPrefab { get; private set; }
    [field:SerializeField] public Transform Content { get; private set; } // Parent of slots with grid layout group.

    public Dictionary<InventorySlot, InventoryUISlot> CurrentSlots { get; private set; } = new Dictionary<InventorySlot, InventoryUISlot>();


    private void Awake()
    {
        Initialize();    
    }


    public virtual void Initialize()
    {
        BuildPanel(GetSlots());

        Inventory.InventoryChangedEventHandler += UpdateUISlot;
    }


    public void BuildPanel(List<InventorySlot> inventorySlots) // First inventory building. Then use UpdateUISlot.
    {
        foreach(var inventorySlot in inventorySlots)
        {
            BuildUISlot(inventorySlot);
        }
    }


    protected virtual List<InventorySlot> GetSlots()
    {
        List<InventorySlot> slots = Inventory.InventorySlots;
        return slots;
    }


    /// <summary>
    /// Called when we modify inventory by adding / removing items. Avoid building / destroying slots every time. (GC optimization).
    /// </summary>
    protected void UpdateUISlot(object sender, EventArgs e)
    {
        var changesData = e as InventoryEventArgs;
        InventorySlot inventorySlot = changesData.InventorySlot;
        bool removed = changesData.Removed;
        bool added = changesData.Added;

        if (added)
        {
            BuildUISlot(inventorySlot);
        }

        else if (removed)
        {
            RemoveUISlot(inventorySlot);
        }

        else // Update values
        {
            CurrentSlots[inventorySlot].UpdateSlot();
        }
    }


    protected virtual void BuildUISlot(InventorySlot inventorySlot)
    {
        var slot = Instantiate(SlotPrefab, Content);
        slot.BuildSlot(inventorySlot);
        CurrentSlots.Add(inventorySlot, slot);
    }


    protected virtual void RemoveUISlot(InventorySlot inventorySlot)
    {
        CurrentSlots.Remove(inventorySlot, out var uiSlot);
        Destroy(uiSlot.gameObject);
    }

}

}
