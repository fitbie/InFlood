using UnityEngine;

namespace Inventory
{

[CreateAssetMenu(fileName = "MaterialItem", menuName = "Inventory/MaterialItem")]
public class MaterialItem : InventoryItem, IMarketable
{
    [field:SerializeField] public float RegPrice { get; set; }
}

}
