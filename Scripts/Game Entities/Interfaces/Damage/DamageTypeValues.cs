using System;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{

    /// <summary>
    /// Base damage unit, containing data about type of damage and its value. ICanDamage contains List of this or more
    /// derived types and reads its value when trying to damage some entity. Also can be inerited, e.g. to implement
    /// effects applying.
    /// </summary>
    [Serializable]
    public class DamageTypeValue
    {
        [field: SerializeField] public DamageType DamageType { get; set; }
        [field: SerializeField] public float DamageValue { get; set; }

        public DamageTypeValue(DamageType damageType, float damageValue)
        {
            DamageType = damageType;
            DamageValue = damageValue;
        }

        /// <summary>
        /// ICanDamage call this before reading DamageType and DamageValue.
        /// Virtual to override and do any manipulations before damage value is readed,
        /// e.g. - handle effects if DamageTypeValue is IEffector, etc..
        /// </summary>
        /// <param name="target"></param>
        public virtual void PreProcess(IDamageable target) {}
    }
    

    [Serializable]
    public class DamageTypeValueWithEffect : DamageTypeValue, IEffector
    {
        [field: SerializeField] public EffectWithChance[] Effects { get; set; }

        public DamageTypeValueWithEffect(DamageType damageType, float damageValue) : 
        base(damageType, damageValue) {}

        public DamageTypeValueWithEffect(DamageType damageType, float damageValue, params EffectWithChance[] effects) : 
        base(damageType, damageValue) 
        {
            Effects = new EffectWithChance[effects.Length];
            Array.Copy(effects, Effects, effects.Length);
        }
        

        public override void PreProcess(IDamageable target)
        {
            ((IEffector)this).TryApplyEffects(target);
        }
    }

}