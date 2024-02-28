namespace InFlood.Entities.ActionSystem
{
    public class EMPStatusEffectData : StatusEffectData
    {
        private readonly IEMPSensitive target;
        private bool isApplied; 


        public EMPStatusEffectData(Effect original, float duration, float delay, IEMPSensitive target) : 
        base(original, duration, delay) => this.target = target;


        public override void Tick()
        {
            if (!isApplied)
            {
                target.ApplyEMP(true);
                isApplied = true;
            }
        }


        public override void RevertEffect() => target.ApplyEMP(false);


        public override string ToString() => $"EMP Effect: Current time: {CurrentTime}, duration: {Duration}, delay: {Delay} ";

    }

}