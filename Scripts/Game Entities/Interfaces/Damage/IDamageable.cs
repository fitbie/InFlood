namespace InFlood.Entities.ActionSystem
{

    public interface IDamageable
    {
        public void TakeDamage(DamageType damageType, float value);
    }

}