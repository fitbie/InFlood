using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Status effects are timed effects.
    /// When ApplyEffect() they may implement some logic, and then construct StatusEffectData.cs instance to be passed to 
    /// StatusEffectsHandler, handler will implement timer and call single StatusEffectData Tick() every Delay second.
    /// </summary>
    public abstract class StatusEffect : Effect
    {
        /// <summary>
        /// Total duration of status effect. StatusEffectData got same Duration.
        /// </summary>
        [field: SerializeField] public float Duration { get; private set; }

        /// <summary>
        /// Effect ticks every Delay second. StatusEffectData got same delay.
        /// </summary>
        [field: SerializeField] public float Delay { get; private set; }
    }

}