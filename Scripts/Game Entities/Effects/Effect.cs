using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Base class for instant and status effects. Representing by SO for memory optimization and other puproses.
    /// Effects are divided into two types: instant and StatusEffect.
    /// Instant effect implement their logic by derive from this class and override ApplyEffect(), 
    /// that is, no base class or handlers.
    /// Status effects are timed effects, they have base (see StatusEffect.cs). 
    /// When ApplyEffect() they may implement some logic, and then construct StatusEffectData.cs instance to be passed to 
    /// StatusEffectsHandler. Usually, all status effect's impact implemented in StatusEffectData.Tick(), that invokes every
    /// float Delay seconds. See StatusEffectData.cs and StatusEffectHandler.cs for additional information.
    /// </summary>
    public abstract class Effect : ScriptableObject
    {
        /// <summary>
        /// Persistent SO, containing effect's icons, text desriptions, etc..
        /// </summary>
        [field: SerializeField] public EffectPersistentData EffectData { get; private set; }

        /// <summary>
        /// When invoked, rather applies changes to source/target (depending on effect implementation) for instant effects,
        /// or handle some logic and constrict StatusEffectData and passes it to target's StatusEffectsHandler for 
        /// StatusEffect.
        /// </summary>
        public abstract void ApplyEffect(object source, object target);

    }

}