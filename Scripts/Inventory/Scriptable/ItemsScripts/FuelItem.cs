using System;
using UnityEngine;

namespace InventorySystem
{

[CreateAssetMenu(fileName = "FuelItem", menuName = "Inventory/FuelItem")]
public class FuelItem : InventoryItem, IMarketable
{
    public enum FuelType { Coal, Petroleum, Uranium  }
    [field:SerializeField] public FuelType fuelType;

    [field:SerializeField] public float RegPrice { get; set; }
}

}
