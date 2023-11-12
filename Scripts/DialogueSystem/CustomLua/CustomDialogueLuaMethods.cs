using System;
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
        Lua.RegisterFunction(nameof(InventoryRemoveItemsOfType), this, SymbolExtensions.GetMethodInfo(() => InventoryRemoveItemsOfType((double)0)));
        // Lua.RegisterFunction(nameof(ContainsEmptySpace), this, SymbolExtensions.GetMethodInfo(() => ContainsEmptySpace()));
    }


    private bool InventoryContainsItem(double itemId, double amount) // Condition
    {
        var inventory = GameManager.Instance.Player.Inventory;
        return inventory.ContainsItem((int)itemId, (int)amount, out var _);
    }


    private bool InventoryRemoveItem(double itemId, double amount)
    {
        var inventory = GameManager.Instance.Player.Inventory;
        return inventory.TryRemoveItem((int)itemId, (int)amount);
    }

    private bool InventoryRemoveItemsOfType(double itemId)
    {
        var inventory = GameManager.Instance.Player.Inventory;
        return inventory.TryRemoveAllItemsOfType((int)itemId);
    }

}

}