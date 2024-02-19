using System.Collections.Generic;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Base interface for any Entity that can deal damage to IDamageable.
    /// Contains List of T, where T is DamageValueType or more derived type.
    /// </summary>
    /// <typeparam name="T">DamageValueType or inherited types, like DamageValueTypeWithEffect.
    /// Represented by List, each element detertmines what type of damage we dealing with and its value.
    /// </typeparam>
    public interface ICanDamage<T> where T: DamageTypeValue
    {
        public List<T> DamageTypes { get; set; }

        public void Damage(IDamageable target)
        {
            foreach (var damage in DamageTypes)
            {
                damage.PreProcess(target);
                target.TakeDamage(damage.DamageType, damage.DamageValue);
            }
        }
    }

}