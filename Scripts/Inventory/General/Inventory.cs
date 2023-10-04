using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

public class Inventory : MonoBehaviour
{
    [field:SerializeField] public List<InventorySlot> InventorySlots { get; private set; } = new List<InventorySlot>();

    public event EventHandler InventoryChangedEventHandler; // Use InventoryEventArgs class below.



    public void AddItem(InventoryItem item, int amount)
    {
        var (contained, index) = ContainsItemWhere(item);
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
    private (bool, int) ContainsItemWhere(InventoryItem item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].Item == item) { return (true, i); }
        }

        return (false, 0);
    }
    

    /// <summary>
    /// Return true if inventory contains item.
    /// </summary>
    /// <param name="item">Item to check</param>
    /// <returns></returns>
    public bool ContainsItem(InventoryItem item)
    {
        for (int i = 0; i < InventorySlots.Count; i++)
        {
            if (InventorySlots[i].Item == item) { return true; }
        }

        return false;
    }


    /// <summary>
    /// Remove item from inventary. True if succes removing, false if failed (no item / too big amount).
    /// </summary>
    /// <param name="item">Item to remove</param>
    /// <param name="amount">Amount to remove</param>
    /// <returns></returns>
    public bool RemoveItem(InventoryItem item, int amount)
    {
        var (contained, index) = ContainsItemWhere(item);
        InventorySlot currentSlot = InventorySlots[index];

        if (!contained || currentSlot.Amount < amount) { return false; } // Failed removing.

        amount = -amount; // We need to distract.
        currentSlot.ChangeAmount(amount);
        if (currentSlot.Amount == 0) { InventorySlots.Remove(currentSlot); }
        
        var inventoryEventArgs = new InventoryEventArgs(currentSlot, amount, currentSlot.Amount);
        InventoryChangedEventHandler?.Invoke(this, inventoryEventArgs);

        return true;
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
