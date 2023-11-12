using System;
using System.Collections.Generic;
using Language.Lua;
using UnityEngine;

namespace InventorySystem
{
[AddComponentMenu("Inventory/Inventory")]
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Representation of entity's items. TODO: Dictionary.
    /// </summary>
    [field:SerializeField] public List<InventorySlot> InventorySlots { get; private set; } = new List<InventorySlot>();

    public event EventHandler InventoryChangedEventHandler; // Use InventoryEventArgs class below.



    public void AddItem(InventoryItem item, int amount)
    {
        ContainsItemWhere(item, out var contained, out var index);

        InventorySlot currentSlot;
        int prevAmount = 0;

        if (contained) // Add amount
        {
            currentSlot = InventorySlots[index];
            prevAmount = currentSlot.Amount; 
            currentSlot.ChangeAmount(amount);
        }
        else // Add new slot
        {
            currentSlot = new InventorySlot(item, amount);
            InventorySlots.Add(currentSlot);
        }
        
        var inventoryEventArgs = new InventoryEventArgs(currentSlot, amount, currentSlot.Amount);
        InventoryChangedEventHandler?.Invoke(this, inventoryEventArgs);
    }

    /// <summary>
    /// Returns tuple of (contain or not item bool, int item index in inventorySlots List)
    /// </summary>
    /// <param name="item">Item to check</param>
    /// <returns></returns>
    private void ContainsItemWhere(InventoryItem item, out bool contain, out int index)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].Item == item) 
            {
                contain = true;
                index = i;
                return;
            }
        }

        contain = false;
        index = 0;
    }
    

    /// <summary>
    /// Return true if inventory contains item.
    /// </summary>
    /// <param name="item">Item to check</param>
    /// <returns></returns>
    public bool ContainsItem(InventoryItem item, int amount, out InventorySlot findedSlot)
    {
        foreach (var slot in InventorySlots)
        {
            if (slot.Item == item && slot.Amount >= amount) 
            {
                findedSlot = slot;
                return true;
            }
        }

        findedSlot = null;
        return false;
    }


    /// <summary>
    /// Return true if inventory contains item with given ID.
    /// </summary>
    /// <param name="id">Item id to check</param>
    /// <returns></returns>
    public bool ContainsItem(int id, int amount, out InventorySlot findedSlot)
    {
        foreach (var slot in InventorySlots)
        {
            if (slot.Item.ID == id && slot.Amount >= amount)
            {
                findedSlot = slot;
                return true;
            }
        }

        findedSlot = null;
        return false;
    }


    /// <summary>
    /// Remove item from inventary. True if succes removing, false if failed (no item / too big amount).
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="amount">Amount to remove</param>
    /// <returns></returns>
    public bool TryRemoveItem(InventoryItem item, int amount)
    {
        ContainsItemWhere(item, out var contained, out var index);
        
        InventorySlot currentSlot = InventorySlots[index];

        if (!contained || currentSlot.Amount < amount) { return false; } // Failed removing.

        RemoveItem(currentSlot, amount);

        return true;
    }


    /// <summary>
    /// Remove item with given ID from inventory. True if succes removing, false if failed (no item / too big amount).
    /// </summary>
    /// <param name="id">Item's ID to remove</param>
    /// <returns></returns>
    public bool TryRemoveItem(int id, int amount)
    {
        if (ContainsItem(id, amount, out var slot))
        {
            RemoveItem(slot, amount);
            return true;
        }   

        return false;
    }

    /// <summary>
    /// Removes all items of given id and return result of deleting.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool TryRemoveAllItemsOfType(int id)
    {
        if (ContainsItem(id, 1, out var slot))
        {
            RemoveItem(slot, slot.Amount);
            return true;
        }

        return false;
    }


    private void RemoveItem(InventorySlot slot, int amount)
    {
        amount = -amount; // We need to distract.
        slot.ChangeAmount(amount);
        if (slot.Amount == 0) { InventorySlots.Remove(slot); }
        
        var inventoryEventArgs = new InventoryEventArgs(slot, amount, slot.Amount);
        InventoryChangedEventHandler?.Invoke(this, inventoryEventArgs);
    }
    
}


public class InventoryEventArgs : EventArgs
{
    public InventorySlot InventorySlot { get; }
    public int AmountChanges { get; }
    public int NewAmount { get; }
    public bool Added { get; }
    public bool Removed { get; }

    public InventoryEventArgs(InventorySlot inventorySlot, int changedAmount, int newAmount)
    {
        InventorySlot = inventorySlot;
        AmountChanges = changedAmount;
        NewAmount = newAmount;
        Added = (NewAmount == AmountChanges && AmountChanges != 0); // We added new item, so changes equale to new amount.
        Removed = (NewAmount == 0 && AmountChanges != 0); // We removed item.
    }
}

}
