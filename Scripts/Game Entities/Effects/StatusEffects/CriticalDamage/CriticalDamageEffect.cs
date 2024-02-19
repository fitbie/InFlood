using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// DamageTypeValue could apply this effect to target, and target will take additional (critical) damage of same Type.
    /// This class seems to work like other StatusEffect, i.e. it constructs CriticalDamageStatusEffectData instance
    /// and pass it to StatusEffectsHandler, and handler invoke Tick() every delay. BUT!
    /// Basically, its not usual StatusEffect, like DamageOverTime. CriticalDamageEffect should have only 1 tick,
    /// i.e. Duration should be equal to Delay. Regards to this we can get UI representation for critical damage
    /// like any other effect, but handling it like instant (one Tick()) effect.
    /// </summary>
    [CreateAssetMenu(fileName = "CriticalDamage", menuName = "InFlood/Effects/Status/CriticalDamage")]
    public class CriticalDamageEffect : StatusEffect
    {
        /// <summary>
        /// Multiplier of income damage. Keep in mind - if its 1 - then 10 start damage become 10 additional 
        /// damage (20 result). If its 2 - 10 start damage become 20 additional damage (30 result).
        /// </summary>
        [field: SerializeField] public float CritValue { get; private set; } = 1;

        public override void ApplyEffect(object source, object target)
        {
            if (source is DamageTypeValue damage && target is IDamageable damageable && target is IEffectable effectable) 
            {
                CriticalDamageStatusEffectData criticalDamage = new(this, Duration, Delay, damageable, damage, CritValue);
                effectable.EffectsHandler.HandleEffect(criticalDamage);
            }
        }
    }

}