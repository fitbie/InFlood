using UnityEngine;
using InventorySystem;

/// <summary>
/// Class for a player entity.
/// </summary>
public class Player : Character // TODO
{



    [field:SerializeField] public Inventory Inventory { get; private set; }
}
