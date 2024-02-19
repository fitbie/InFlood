using UnityEngine;

namespace InFlood.Entities.Modules
{

/// <summary>
/// Instantiate, controls, initializes, remove and do other stuff with Module.
/// </summary>
[AddComponentMenu("Entities/Modules/ModuleSlot")]
public class ModuleSlot : MonoBehaviour
{
    /// <summary>
    /// Slot's unique ID. TODO: remove
    /// </summary>
    // [field: SerializeField] public int ID { get; private set; }

    /// <summary>
    /// Module that we assign to slot.
    /// </summary>
    [field: SerializeField] public Module Module { get; protected set; }

    /// <summary>
    /// Slot can contain module of this size.
    /// </summary>
    [field: SerializeField] public ModuleSize ModuleSize { get; private set; }

    /// <summary>
    /// Slot can contain module of this type. For additional information check ModuleType in Module.cs.
    /// </summary>
    [field: SerializeField] public ModuleType ModuleType { get; private set; }

    [SerializeField] private bool drawGizmos = true;
    
    

    private void OnValidate() // Editor only. Checks for adding module size matching.
    {
        if (Module != null && (Module.Size != ModuleSize || Module.ModuleType != ModuleType))
        {
            var temp = Module;
            Module = null;
            throw new ModuleAddRemoveExeption($"{temp.gameObject.name} mismatch {gameObject.name} Module size or type!!");
        }    
    }

    
    public void Initialize(Entity owner)
    {
        if (Module != null) { Module.Initialize(owner); }
    }


    public Module SetModule(Module modulePrefab, Entity owner)
    {
        var result = Instantiate(modulePrefab, transform);
        Module = result;
        Module.Initialize(owner);
        return Module;
    }


    public bool ResetModule()
    {
        if (Module == null) 
        { 
            throw new ModuleAddRemoveExeption
            ($"Can't reset {gameObject.name} ModuleSlot: {gameObject.name} Module is null!", Module);
        }

        Module.Terminate();
        Destroy(Module.gameObject);
        Module = null;
        return true;
    }

    
    private void OnDrawGizmosSelected() // For understanding where future module will be placed.
    {
        if (!drawGizmos) { return; }
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }


    private void OnDestroy()
    {
        if (Module != null) { Module.Terminate(); }    
    }
}

}