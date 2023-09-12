using UnityEngine;

namespace Inventory
{

/// <summary>
/// Temp class for testing, TODO REFACTOR
/// </summary>
public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItem[] items;
    [SerializeField] private int amount;
    [SerializeField] private Inventory inventory; // TODO REFACTOR



    public void AddItem()
    {
        foreach (var item in items)
        {
            inventory.AddItem(item, amount);
        }
    }


    public void RemoveItem()
    {
        foreach (var item in items)
        {
            inventory.RemoveItem(item, amount);
        }
    }
}

}
