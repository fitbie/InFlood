using UnityEngine;

namespace InFlood.Entities.Modules
{

/// <summary>
/// Contains static info about module, e.g. description, icon, etc..
/// </summary>
[CreateAssetMenu(fileName = "ModulePersistentData", menuName = "InFlood/Modules/ModulePersistentData")] 
public class ModulePersistentData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public Sprite BigIcon { get; private set; }
    [field: SerializeField] public Sprite SmallIcon { get; private set; }

}

}