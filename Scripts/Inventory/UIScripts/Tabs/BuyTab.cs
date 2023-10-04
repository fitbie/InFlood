using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

using DisplayMode = InventoryUISlot.DisplayMode;

public class BuyTab : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public static void ShowBuySlots(InventoryPanelBuilder inventoryPanel, List<InventorySlot> merchantSlots)
    {
        var currentSlots = inventoryPanel.CurrentSlots;
        foreach (var element in currentSlots)
        {
            var uiSlot = element.Value;
            uiSlot.gameObject.SetActive(false);

            
        }
    }

}

}