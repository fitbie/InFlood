namespace InFlood.Entities.ActionSystem
{
    public class EMPStatusEffectData : StatusEffectData
    {
        private readonly IEMPSensitive target;


        public EMPStatusEffectData(Effect original, float duration, float delay, IEMPSensitive target) : 
        base(original, duration, delay)
        {
            this.target = target;
            target.ApplyEMP(true);
        }

        public override void Tick()
        {
            // No need to tick for EMP.
        }


        public override void RevertEffect()
        {
            target.ApplyEMP(false);
        }


        public override string ToString()
        {
            return $"EMP Effect: Current time: {CurrentTime}, duration: {Duration}, delay: {Delay} ";
        }

    }

}