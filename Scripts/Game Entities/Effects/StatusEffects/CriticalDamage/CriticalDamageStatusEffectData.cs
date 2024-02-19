namespace InFlood.Entities.ActionSystem
{
    public class CriticalDamageStatusEffectData : StatusEffectData
    {
        private DamageTypeValue damageTypeValue;
        private IDamageable target;
        private float criticalValue;


        public CriticalDamageStatusEffectData
        (Effect original, float duration, float delay, IDamageable damageable, DamageTypeValue damage, float critical) : 
        base(original, duration, delay)
        {
            damageTypeValue = new(damage.DamageType, damage.DamageValue);
            target = damageable;
            criticalValue = critical;
        }

        public override void Tick()
        {
            target.TakeDamage(damageTypeValue.DamageType, damageTypeValue.DamageValue * criticalValue);
        }
    }

}