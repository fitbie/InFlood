using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Describes effect's persistend data - UI, description, etc..
    /// </summary>
    [CreateAssetMenu(fileName = "EffectPersistentData", menuName = "InFlood/Effects/EffectPersistentData")] 
    public class EffectPersistentData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
    }

}