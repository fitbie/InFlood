using UnityEngine;

namespace InventorySystem
{

[CreateAssetMenu(fileName = "MaterialItem", menuName = "Inventory/MaterialItem")]
public class MaterialItem : InventoryItem, IMarketable
{
    [field:SerializeField] public float RegPrice { get; set; }
}

}
