using System;
using UnityEngine;

namespace InventorySystem
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


    public void ChangeAmount(int value)
    {
        Amount += value;
    }
}

}
