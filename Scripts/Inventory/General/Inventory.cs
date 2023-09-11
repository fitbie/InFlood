using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

public class Inventory : MonoBehaviour
{
    [field:SerializeField] public List<InventorySlot> InventorySlots { get; private set; } = new List<InventorySlot>();

    #region Callbacks

    public event Action<InventorySlot, int> ItemAmountAdded;
    public event Action<InventorySlot, int> ItemAmountRemoved;
    public event Action<InventorySlot> ItemDestroyed;
    public event Action ItemRemovedFail;

    #endregion



    public void AddItem(InventoryItem item, int amount)
    {
        var (contained, index) = ContainsItemWhere(item);
        InventorySlot currentSlot;
        if (contained)
        {
            currentSlot = InventorySlots[index];
            currentSlot.AddAmount(amount);
        }
        else
        {
            currentSlot = new InventorySlot(item, amount);
            InventorySlots.Add(currentSlot);
        }

        ItemAmountAdded?.Invoke(currentSlot, amount);
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
    /// Return true if inventory contain such item.
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


    public bool RemoveItem(InventoryItem item, int amount) // True - successed removing, false - failed.
    {
        var (contained, index) = ContainsItemWhere(item);
        InventorySlot currentSlot = InventorySlots[index];

        if (contained && currentSlot.Amount > amount)
        {
            InventorySlots[index].RemoveAmount(amount);
            ItemAmountRemoved?.Invoke(currentSlot, amount);

            return true;
        }
        else if (currentSlot.Amount == amount)
        {
            InventorySlots.Remove(currentSlot);
            ItemDestroyed?.Invoke(currentSlot);

            return true;
        }
        else
        {
            ItemRemovedFail?.Invoke();

            return false;
        }
    }
}




[Serializable]
public class InventorySlot
{
    [field:SerializeField] public InventoryItem Item { get; private set; }
    [field:SerializeField] public int Amount { get; private set; }

    public InventorySlot(InventoryItem item, int amount)
    {
        Item = item;
        Amount = amount;
    }


    public void AddAmount(int value)
    {
        Amount += value;
    }

    public void RemoveAmount(int value)
    {
        Amount -= value;
    }
}

}
