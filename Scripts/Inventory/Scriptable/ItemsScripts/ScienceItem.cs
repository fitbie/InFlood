using UnityEngine;

namespace InventorySystem
{

[CreateAssetMenu(fileName = "ScienceItem", menuName = "Inventory/ScienceItem")]
public class ScienceItem : InventoryItem, IMarketable
{
    [field:SerializeField] public float RegPrice { get; set; }
}

}
