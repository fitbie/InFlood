using UnityEngine;


namespace InventorySystem
{

/// <summary>
/// Base class for inventory items.
/// </summary>
[CreateAssetMenu(fileName = "BaseItem", menuName = "Inventory/Base Item")] 
public class InventoryItem : ScriptableObject
{
    #region Properties

    [field:SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public GameObject ContainerPrefab { get; private set; } // Prefab for spawned loot box
    [field: SerializeField] public int Rarety { get; private set; } // Loot raraty
    [field: SerializeField] public Sprite SmallIcon { get; private set; } // Icon for inventory slot.
    [field: SerializeField] public Sprite BigIcon { get; private set; } // Icon for item panel.
    [field: SerializeField] public Color TypeColor { get; private set; } // Color for UI slot background + some FX.
    [field:SerializeField] public float Weight { get; private set; }

    #endregion
}

}
