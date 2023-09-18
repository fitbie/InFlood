using UnityEngine;

namespace Inventory
{

/// <summary>
/// Scene class for adding item.
/// </summary>
[AddComponentMenu("Inventory/Add Inventory Item")]
public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory; // TODO GET REFERENCE
    [SerializeField] private InventorySlot[] itemsToAdd;



    public void AddItems()
    {
        if (itemsToAdd.Length == 0) { return; }
        foreach (var item in itemsToAdd)
        {
            inventory.AddItem(item.Item, item.Amount);
        }
    }

}

}
