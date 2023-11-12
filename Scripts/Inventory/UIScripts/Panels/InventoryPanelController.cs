using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

/// <summary>
/// Base UI inventory panel class to derive from.
/// </summary>
[AddComponentMenu("Inventory/Base Inventory Panel")]
public class InventoryPanelController : MonoBehaviour
{
    [field:SerializeField] public Inventory Inventory { get; protected set; } // Need to get acces in children.
    
    [field:SerializeField] public InventoryUISlot SlotPrefab { get; private set; }
    [field:SerializeField] public Transform SlotContainer { get; private set; } // Parent of slots with grid layout group.

    public Dictionary<InventorySlot, InventoryUISlot> CurrentSlots { get; private set; } = new Dictionary<InventorySlot, InventoryUISlot>();

    


    private void Awake()
    {
        Initialize();    
    }


    public virtual void Initialize()
    {
        BuildPanel(Inventory.InventorySlots);

        Inventory.InventoryChangedEventHandler += UpdateUISlot;
    }


    protected virtual void BuildPanel(List<InventorySlot> inventorySlots) // First inventory building. Then use UpdateUISlot.
    {
        foreach(var inventorySlot in inventorySlots)
        {
            AddUISlot(inventorySlot, SlotContainer, out var uISlot);
        }
    }


    protected virtual void AddUISlot(InventorySlot slotData, Transform container, out InventoryUISlot addedSlot)
    {
        var slot = Instantiate(SlotPrefab, container);
        slot.BuildSlot(slotData);
        addedSlot = slot;
        CurrentSlots.Add(slotData, addedSlot);
    }


    protected virtual void RemoveUISlot(InventorySlot inventorySlot)
    {
        CurrentSlots.Remove(inventorySlot, out var uiSlot);
        GameObject.Destroy(uiSlot.gameObject);
    }


    /// <summary>
    /// Called when we modify inventory by adding / removing items. Avoid building / destroying slots every time. (GC optimization).
    /// </summary>
    protected virtual void UpdateUISlot(object sender, EventArgs e)
    {
        var changesData = e as InventoryEventArgs;
        InventorySlot inventorySlot = changesData.InventorySlot;
        bool removed = changesData.Removed;
        bool added = changesData.Added;

        if (added)
        {
            AddUISlot(inventorySlot, SlotContainer, out var _);
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



}

}
