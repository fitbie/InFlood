namespace InFlood.Entities.ActionSystem
{

    public class DOTStatusEffectData : StatusEffectData
    {
        public DamageTypeValue Damage { get; protected set; }
        protected IDamageable target;


        public DOTStatusEffectData(Effect original, float duration, float delay, IDamageable target, DamageTypeValue damage) :
        base(original, duration, delay)
        {
            Damage = new DamageTypeValue(damage.DamageType, damage.DamageValue);
            this.target = target;
        }

        public override void Tick()
        {
            target.TakeDamage(Damage.DamageType, Damage.DamageValue);
        }

        public override bool Equals(object obj)
        {
            return obj is DOTStatusEffectData dotsed && dotsed.Damage.DamageType == Damage.DamageType;
        }

        public override int GetHashCode()
        {
            return Damage.DamageType.GetHashCode();
        }

        public override void Concat(StatusEffectData other)
        {
            if (other is not DOTStatusEffectData dotsed || dotsed.Damage.DamageType != Damage.DamageType) { return; }
            float currentTicks = CurrentTime / Delay; // How many Tick()'s are lefted for current effect?
            float otherTicks = dotsed.Duration / dotsed.Delay; // How many Tick()'s are lefted for other effect?
            
            CurrentTime += dotsed.Duration; // Prolong current time by other duration.
            Delay = CurrentTime / (currentTicks + otherTicks); // Calculate median delay so we get all of Tick()'s.

            // Calculate median damage value required to deal same damage, if two effect were ticking separately. 
            Damage.DamageValue = (Damage.DamageValue * currentTicks + dotsed.Damage.DamageValue * otherTicks) / 
            (currentTicks + otherTicks);

            Duration += other.Duration;
        }


        public override string ToString()
        {
            return $"DOT Effect: Damage: {Damage.DamageType} {Damage.DamageValue}, current time: {CurrentTime}, duration: {Duration}, delay: {Delay} ";
        }

    }

}