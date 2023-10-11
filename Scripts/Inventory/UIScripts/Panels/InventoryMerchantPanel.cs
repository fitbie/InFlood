using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

/// <summary>
/// This class builds a UI panel of merchant items in the player's inventory, since the merchant has a separate inventory.
/// </summary>
public class InventoryMerchantPanel : InventoryPanelController
{
    public override void Initialize()
    {
        Merchant.OnMerchantTriggered += UpdateMerchantPanel;
    }


    /// <summary>
    /// This method called every time we enter merchant trigger and calls Merchant slots rebuilding,
    /// because there are more then 1 merchant.
    /// </summary>
    /// <param name="currentMerchant">Merchant we triggered</param>
    private void UpdateMerchantPanel(Merchant currentMerchant)
    {
        if (currentMerchant.IsTriggered)
        {
            Inventory = currentMerchant.Inventory;
            BuildPanel(Inventory.InventorySlots);
            Inventory.InventoryChangedEventHandler += UpdateUISlot;
        }
        else
        {
            if (CurrentSlots.Count != 0)
            {
                InventorySlot[] slots = new InventorySlot[CurrentSlots.Keys.Count];
                CurrentSlots.Keys.CopyTo(slots, 0);
                for (int i = 0; i < slots.Length; i++) // Delete UI GameObjects.
                {
                    RemoveUISlot(slots[i]);
                }
            }

            Inventory.InventoryChangedEventHandler -= UpdateUISlot;
        }
    }


        protected override void AddUISlot(InventorySlot slotData, Transform container, out InventoryUISlot addedSlot)
        {
            base.AddUISlot(slotData, container, out addedSlot);
            addedSlot.gameObject.SetActive(InventoryTabsController.currentTab is BuyTab);
        }

    }

}
