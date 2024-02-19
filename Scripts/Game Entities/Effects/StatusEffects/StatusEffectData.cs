namespace InFlood.Entities.ActionSystem
{   
    /// <summary>
    /// Base for temporary data classes, representing status(timed) effect data. 
    /// When status effect is applied, Effect class (SO) contsructs
    /// instance of this and passes it to IEffectable.StatusEffectsHandler. Basically, this effects contains reference to its
    /// original Effect and remaining effect time. Also overrides Equals(), GetHashCode() and + operator, in case of same
    /// effects get applied.
    /// StatusEffectsHandler keeps List of current StatusEffectData's.
    /// </summary>
    [System.Serializable]
    public abstract class StatusEffectData
    {
        /// <summary>
        /// Original (Effect SO) of this effect data. Using to reference any data, like PersistentEffectData for UI.
        /// </summary>
        public Effect Original { get; init; }

        /// <summary>
        /// Duration in seconds of effect.
        /// </summary>
        public float Duration { get; protected set; }

        /// <summary>
        /// Current time of effect, when effect is applied its equal to Duration.
        /// </summary>
        public float CurrentTime { get; set; }

        /// <summary>
        /// Every Delay seconds StatusEffectsHandler invoke Tick() while effect is active.
        /// </summary>
        /// <value></value>
        public float Delay { get; protected set; }

        public StatusEffectData(Effect original, float duration, float delay)
        {
            Original = original;
            Duration = duration;
            CurrentTime = Duration;
            Delay = delay;
        }


        /// <summary>
        /// WARNING!
        /// All status effect data are using in hash structures, like HashSet or Dictionaries. Example: check for already
        /// existing same effect in StatusEffectsHandler, and if it is - concatenate two effects, i.e. prolong effect's
        /// CurrentTime and do math with some values like damage for DOT effects. If effect is unique - like EMPulse - 
        /// we can use default implementation, i.e. compare effectdata by their Original.GetType(), 
        /// and get Original.GetType() hashcode.
        /// For more complex StatusEffectData - more complex logic, e.g. DOTStatusEffectData gets hashcode by 
        /// its DamageType and compares by it too. 
        /// Do not use StatusEffectData Duration, CurrentTime, or orher mutable data. 
        /// </summary>
        public override int GetHashCode()
        {
            return Original.GetType().GetHashCode();
        }


        /// <summary>
        /// StatusEffectData's by default are using their Original StatusEffect GetHashCode() and Equals() to compare and
        /// hashing. So we implement it by comparing types and get types hashcode, i.e. if two StatusEffectData's have
        /// common original StatusEffect SO Type - they are equal.
        /// Strongly recommended for some StatusEffectData's implement their own GetHashCode() and Equals(), because
        /// it will be used in hashcode-dependent structures. Example of implementation: DOTStatusEffectData compares by 
        /// their DamageType enum (i.e. constant).
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is StatusEffectData effectData && Original.GetType() == effectData.Original.GetType(); 
        }

        /// <summary>
        /// Invoke single tick of effect. E.g. - for DamageOverTime effect it's deal damage to target every Tick().
        /// </summary>
        public abstract void Tick();

        /// <summary>
        /// Concatenate (sum) other effect to current.
        /// By default - prolongs effect's CurrentTime by other effect's Duration, and computing median Delay.
        /// Can be overriden, e.g. for DamageOverTime effects its should be same DamageType, 
        /// so Concat() prolongs CurrentTime and computes median damage and delay.
        /// </summary>
        /// <param name="other">Other(Equals) effect for concatenation.</param>
        public virtual void Concat(StatusEffectData other)
        {
            float currentTicks = CurrentTime / Delay; // How many Tick()'s are lefted for current effect?
            float otherTicks = other.Duration / other.Delay; // How many Tick()'s are lefted for other effect?
            
            CurrentTime += other.Duration; // Prolong current time by other duration.
            Delay = CurrentTime / (currentTicks + otherTicks); // Calculate median delay so we get all of Tick()'s.
            Duration += other.Duration;
        }

        /// <summary>
        /// When CurrentTime is below 0 - revert any changes, if they have been applied temporary, e.g. - EMPulse.
        /// </summary>
        public virtual void RevertEffect() {}
    }

}