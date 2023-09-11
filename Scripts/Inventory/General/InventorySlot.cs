using System;
using UnityEngine;

namespace Inventory
{

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
