using UnityEngine;

namespace Inventory
{

[CreateAssetMenu(fileName = "FoodItem", menuName = "Inventory/FoodItem")]
public class FoodItem : InventoryItem, IMarketable
{
    [field:SerializeField] public float RegPrice { get; set; }
}

}
