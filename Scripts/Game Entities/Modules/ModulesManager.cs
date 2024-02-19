using System.Collections.Generic;
using UnityEngine;

namespace InFlood.Entities.Modules
{
/// <summary>
/// Controls and operate adding / removing / initializing / etc. entity modules in module slots
/// (weapon, engine, abilities, etc.).
/// </summary>
[AddComponentMenu("Entities/Modules/ModulesManager")]
public class ModulesManager : MonoBehaviour
{
    public Entity Owner { get; set; }

    /// <summary>
    /// All ModuleSlots on this entity, ModuleSlot could contain Module.
    /// </summary>
    [field: SerializeField] public ModuleSlot[] ModuleSlots { get; private set; }

    // For fast searching & adding modules by ModuleType value.
    private Dictionary<ModuleType, List<ModuleSlot>> SlotsByTypeDictionary { get; set; } = 
    new Dictionary<ModuleType, List<ModuleSlot>>();



    public void Initialize(Entity owner)
    {
        Owner = owner;
        
        foreach (var slot in ModuleSlots)
        {
            if (!SlotsByTypeDictionary.ContainsKey(slot.ModuleType))
            {
                SlotsByTypeDictionary.Add(slot.ModuleType, new List<ModuleSlot>());
            }

            SlotsByTypeDictionary[slot.ModuleType].Add(slot);

            slot.Initialize(Owner);
        }
    }


    /// <summary>
    /// Try add module to Ship's ModuleManager.
    /// </summary>
    /// <param name="modulePrefab">Module Prefab to add</param>
    /// <returns></returns>
    public bool TryAddModule(Module modulePrefab)
    {
        var slotList = GetSlotListByType(modulePrefab);

        return AddElement(slotList, modulePrefab);

        bool AddElement(List<ModuleSlot> moduleSlots, Module module)
        {
            for (int i = 0; i < moduleSlots.Count; i++)
            {
                if 
                (moduleSlots[i].Module == null && moduleSlots[i].ModuleSize == module.Size)
                { 
                    moduleSlots[i].SetModule(module, Owner);
                    return true;
                }
            }
            
            return false;
            // throw new ModuleAddRemoveExeption
            // ($"No suitable slot for {module.gameObject.name} on {Owner.gameObject.name}",
            // this, module);
        }
    }


    /// <summary>
    /// Removes module from chosen ModuleSlot.
    /// </summary>
    /// <param name="slot">ModuleSlot to be cleared from Module.</param>
    /// <returns></returns>
    public bool TryRemoveModule(ModuleSlot slot)
    {
        return slot.ResetModule();
    }


    /// <summary>
    /// Not recommended! Remove module by ModuleType.
    /// </summary>
    /// <param name="module"></param>
    /// <returns></returns>
    // public bool TryRemoveModuleByType(Module module)
    // {
    //     if (GetSlotListByType(module, out var slotList))
    //     {
    //         foreach (var slot in slotList)
    //         {
    //             if (slot.Module != null && slot.Module.GetType() == module.GetType())
    //             {
    //                 return slot.ResetModule();
    //             }
    //         }
    //     }

    //     throw new ModuleAddRemoveExeption
    //     ($"Cannot remove {module.gameObject.name} on {Owner.gameObject.name}",
    //     this, module);
    // }


    private List<ModuleSlot> GetSlotListByType(Module module)
    {
        var moduleType = module.ModuleType;

        if (SlotsByTypeDictionary.TryGetValue(moduleType, out var list))
        {
            return list;
        }
        
        throw new NoSlotListForModuleTypeExeption
                ($"{Owner.gameObject.name} ModuleManager doesn't contains slots for {moduleType}!",
                this, module);
    }
}

}
