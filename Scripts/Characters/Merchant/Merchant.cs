using System;
using InventorySystem;
using UnityEngine;


/// <summary>
/// Class that implements the logic of merchants. Selling / buying / activating sale/buy panels in inventory UI.
/// </summary>
public class Merchant : MonoBehaviour
{
    /// <summary>
    /// This property stores a reference to the current merchant we are interacting with.
    /// Alternatively, the same data can be stored in the Player class in case of multiplayer.
    /// </summary>
    /// <value></value>
    public static Merchant Current { get; private set; }

    public static event Action<Merchant> OnMerchantTriggered;

    [field: SerializeField] public Inventory Inventory { get; private set; }
    public bool IsTriggered { get; private set; } = false;
    private Player player;

    
    public void MerchantTriggered(GameObject other) // Called from TriggerEvent component.
    {
        if (!other.TryGetComponent<Player>(out Player currentplayer)) { return; }
        player = currentplayer;

        IsTriggered = true;
        Current = this;
        OnMerchantTriggered(this);
    }


    public void MerchantUntriggered(GameObject other) // Called from TriggerEvent component.
    {
        if (!other.TryGetComponent<Player>(out Player player)) { return; }
        player = null;

        IsTriggered = false;
        Current = null;
       OnMerchantTriggered(this);
    }


    /// <summary>
    /// Removes item from player(target) inventory and adds it to merchant inventory + money logic.
    /// </summary>
    /// <param name="slot">Item to sell</param>
    public void SellItem(InventorySlot slot)
    {
        var item = slot.Item;
        var amount = slot.Amount;
        player.Inventory.TryRemoveItem(item, 1);

        Inventory.AddItem(item, 1); // TODO: hold pointer + slider how much items sell.

        //TODO: add money transfer
    }

    
    /// <summary>
    /// Removes item from merchant inventory and adds it to player(target) inventory + money logic.
    /// </summary>
    /// <param name="slot">Item to buy</param>
    public void BuyItem(InventorySlot slot)
    {
        var item = slot.Item;
        var amount = slot.Amount;
        Inventory.TryRemoveItem(item, 1);

        player.Inventory.AddItem(item, 1); // TODO: hold pointer + slider how much items sell.

        //TODO: add money transfer
    }
}
