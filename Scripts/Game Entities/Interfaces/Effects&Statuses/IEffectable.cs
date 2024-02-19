namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Provides handler for status(temporary) effects handling.
    /// </summary>
    public interface IEffectable
    {
        public StatusEffectsHandler EffectsHandler { get; set; }
    }

}