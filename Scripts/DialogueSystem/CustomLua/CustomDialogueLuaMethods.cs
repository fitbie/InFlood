using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace DialogueSystemCustom
{

[AddComponentMenu("")]
public class CustomDialogueLuaMethods : MonoBehaviour
{
    void Awake() 
    {
        Lua.RegisterFunction(nameof(InventoryContainsItem), this, SymbolExtensions.GetMethodInfo(() => InventoryContainsItem((double)0, (double)0)));
        Lua.RegisterFunction(nameof(InventoryRemoveItem), this, SymbolExtensions.GetMethodInfo(() => InventoryRemoveItem((double)0, (double)0)));
        // Lua.RegisterFunction(nameof(ContainsEmptySpace), this, SymbolExtensions.GetMethodInfo(() => ContainsEmptySpace()));
    }


    private bool InventoryContainsItem(double itemId, double amount) // Condition
    {
        var inventory = GameManager.Instance.Player.Inventory;
        return inventory.ContainsItem((int)itemId, (int)amount);
    }


    private bool InventoryRemoveItem(double itemId, double amount)
    {
        var inventory = GameManager.Instance.Player.Inventory;
        return inventory.TryRemoveItem((int)itemId, (int)amount);
    }

    // private bool ContainsEmptySpace()
    // {
    //     
    // }

}

}