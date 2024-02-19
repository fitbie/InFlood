using System;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Wrapper struct containing Effect & its chance to be applied.
    /// </summary>
    [System.Serializable]
    public struct EffectWithChance
    {
        /// <summary>
        /// Applied effect.
        /// </summary>
        [field: SerializeField] public Effect Effect { get; private set; }

        [SerializeField]
        [Range(0, 1)]
        private float effectChance;
        /// <summary>
        /// Effect's chance to be applied.
        /// </summary>
        public float EffectChance => effectChance;

        public EffectWithChance(Effect effect, float chance)
        {
            Effect = effect;
            effectChance = chance;
        }
    }


    /// <summary>
    /// Provides Effect applying for implementating entity.
    /// E.g. - ICanDamage<DamageTypeValueWithEffect> could apply fire per second effect when damage.
    /// </summary>
    public interface IEffector
    {
        /// <summary>
        /// Applied effects and its chances to be applied.
        /// </summary>
        public EffectWithChance[] Effects { get; set; }


        /// <summary>
        /// Try to cause effect using Random.value and EffectChance.
        /// </summary>
        /// <param name="target">Target of effect, e.g. IDamageable in case of effect from weapon.</param>
        public void TryApplyEffects(object target)
        {
            foreach (var effect in Effects)
            {
                if (effect.Effect == null) { continue; }
                
                if (UnityEngine.Random.value <= effect.EffectChance) 
                { effect.Effect.ApplyEffect(this, target); }
            }
        }
    }

}