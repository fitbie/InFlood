using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{

public class InventoryUISlotsSorter
{


    #region TypeSorting

    // Determines in which order the sorting will proceed. If we add new item typed - update this list.
    private static List<Type> typeOrder = new List<Type>
    {
        typeof(QuestItem),
        typeof(FoodItem),
        typeof(FuelItem),
        typeof(ScienceItem),
        typeof(MaterialItem),
        typeof(LuxuryItem),
    };


    public static void SortChildrenByItemType(Dictionary<InventorySlot, InventoryUISlot> currentSlots)
    {
        // Get all keys(InventorySlot) and order them using typeOrder.
        var sortedKeys = currentSlots.Keys.OrderBy(key => typeOrder.IndexOf(key.Item.GetType()));

        // Move slots position accordong to sordedKeys
        foreach (var key in sortedKeys)
        {
            var uiSlot = currentSlots[key];
            uiSlot.transform.SetAsLastSibling();
        }
    }

    #endregion

}

}
