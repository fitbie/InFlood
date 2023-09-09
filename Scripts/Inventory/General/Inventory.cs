using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

public class Inventory : MonoBehaviour
{
    [field:SerializeField] public List<InventorySlot> InventorySlots { get; private set; } = new List<InventorySlot>();

    #region Callbacks

    public Action<InventorySlot, int> itemAdded;
    public Action<InventorySlot, int> itemRemovedSucces;
    public Action itemRemovedFail;

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

        itemAdded?.Invoke(currentSlot, amount);
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
            itemRemovedSucces?.Invoke(currentSlot, amount);

            return true;
        }
        else if (currentSlot.Amount == amount)
        {
            InventorySlots.Remove(currentSlot);
            itemRemovedSucces?.Invoke(currentSlot, amount);

            return true;
        }
        else
        {
            itemRemovedFail?.Invoke();

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
