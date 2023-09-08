using UnityEngine;


namespace Inventory
{

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    #region Properties

    [field:SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite SmallIcon { get; private set; }
    [field: SerializeField] public Sprite BigIcon { get; private set; }
    [field: SerializeField] public float RegPrice { get; private set; }
    [field:SerializeField] public float Weight { get; private set; }

    #endregion
}

}
