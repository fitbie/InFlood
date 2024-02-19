namespace InFlood.Entities.ActionSystem
{

    /// <summary>
    /// Interface, describing Entity's behaviour when electromagnetic pulse (EMP) is applied.
    /// Example: Ship are EMPSensitive. When EMP is applied, Ships disable random amount of modules in their
    /// IHAveModues.ModulesMaanger. Also, EMP usually is timed effect. So, if EMP changes need to be temporary,
    /// implement IEffectable for target. 
    /// </summary>
    public interface IEMPSensitive
    {
        /// <summary>
        /// Applies EMP to Entity. E.g. turn off modules, navigation, etc..
        /// </summary>
        /// <param name="status">True - set EMP effects, false - reset</param>
        public void ApplyEMP(bool status);
    }

}