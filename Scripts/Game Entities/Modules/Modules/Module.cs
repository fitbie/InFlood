using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules
{

/// <summary>
/// Size of module for game balance - Small, Medium, Large. 
/// </summary>
public enum ModuleSize { S, M, L }

/// <summary>
/// Enum representation of Module type, e.g. Weapon, Engine, etc.. We could simply use System.Type and generics 
/// like ModuleSlot(T) where T: Module, but since Unity can't serialize it - we forced to 
/// use enum to define module type for ModuleSlot and stuff. 
/// </summary>
public enum ModuleType { Weapon, Protection, Ability, Engine, Sail }

/// <summary>
/// Base class for ship modules, e.g. weapon, engine, etc..
/// </summary>
public abstract class Module : MonoBehaviour
{
    [field:SerializeField] public ModuleSize Size { get; set; }

    public ModuleType ModuleType
    {
        get
        {
            return this switch
            {
                WeaponModule => ModuleType.Weapon,
                ProtectionModule => ModuleType.Protection,
                AbilityModule => ModuleType.Ability,
                EngineModule => ModuleType.Engine,
                SailModule => ModuleType.Sail,
                _ => throw new System.Exception
                ($"Failed to get {gameObject.name} ModuleType! Check Module.ModuleType and add new types if it's nessesery!"),
            };
        }   
    }
    

    [field:SerializeField] public ModulePersistentData ModuleInfoData { get; private set; } 
    
    /// <summary>
    /// Module's owner Entity.
    /// </summary>
    /// <value></value>
    protected Entity Owner { get; set; }
    public UnityEvent OnInitialize;
    public UnityEvent OnTerminate;

    public virtual void Initialize(Entity owner) 
    {
        Owner = owner;
        OnInitialize?.Invoke();
    }
    
    public virtual void Terminate() => OnTerminate?.Invoke();
    
}

}