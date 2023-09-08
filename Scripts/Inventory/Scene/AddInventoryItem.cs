using UnityEngine;

namespace Inventory
{

/// <summary>
/// Temp class for testing, TODO REFACTOR
/// </summary>
public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    [SerializeField] private int amount;
    [SerializeField] private Inventory inventory; // TODO REFACTOR



    public void AddItem()
    {
        inventory.AddItem(item, amount);
    }


    public void RemoveItem()
    {
        inventory.RemoveItem(item, amount);
    }
}

}
