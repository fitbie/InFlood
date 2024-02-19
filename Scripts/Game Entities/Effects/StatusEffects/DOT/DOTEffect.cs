using System;
using System.Collections.Generic;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Damage Over Time version of StatusEffect. For each DamageTypeValue in DamageTypes List this class construct new
    /// DOTStatusEffectData with specified DamageType and value. And than DOTStatusEffectData deals damage to target 
    /// IDamageable every Tick(). 
    /// </summary>
    [CreateAssetMenu(fileName = "DOTEffect", menuName = "InFlood/Effects/Status/DOTEffect")]
    public class DOTEffect : StatusEffect, ICanDamage<DamageTypeValue>
    {
        [field: SerializeField] public List<DamageTypeValue> DamageTypes { get; set; }
        public Action OnDamage { get; set; }
        protected IDamageable damageableTarget;


        public override void ApplyEffect(object source, object target)
        {
            if (target is IDamageable damageable && target is IEffectable effectable)
            {
                foreach (var damage in DamageTypes)
                {
                    if (damage != null)
                    {
                        DOTStatusEffectData effectData = new(this, Duration, Delay, damageable, damage);
                        effectable.EffectsHandler.HandleEffect(effectData);
                    }
                }
            }
        }

    }

}