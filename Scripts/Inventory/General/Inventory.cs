using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    #region Callbacks

    public Action<InventoryItem, int> itemAdded;
    public Action<InventoryItem, int> itemRemovedSucces;
    public Action itemRemovedFail;

    #endregion



    public void AddItem(InventoryItem item, int amount)
    {
        var (contained, index) = ContainsItemWhere(item);
        if (contained)
        {
            inventorySlots[index].AddAmount(amount);
        }
        else
        {
            inventorySlots.Add(new InventorySlot(item, amount));
        }

        itemAdded?.Invoke(item, amount);
    }

    /// <summary>
    /// Returns tuple of (contain or not item bool, int item index in inventorySlots List)
    /// </summary>
    /// <param name="item">Item to check</param>
    /// <returns></returns>
    private (bool, int) ContainsItemWhere(InventoryItem item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item == item) { return (true, i); }
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
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].Item == item) { return true; }
        }

        return false;
    }


    public void RemoveItem(InventoryItem item, int amount)
    {
        var (contained, index) = ContainsItemWhere(item);
        var currentSlot = inventorySlots[index];

        if (contained && currentSlot.Amount > amount)
        {
            inventorySlots[index].RemoveAmount(amount);

            itemRemovedSucces?.Invoke(item, amount);
        }
        else if (currentSlot.Amount == amount)
        {
            inventorySlots.Remove(currentSlot);
            itemRemovedSucces?.Invoke(item, amount);
        }
        else
        {
            itemRemovedFail?.Invoke();
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
