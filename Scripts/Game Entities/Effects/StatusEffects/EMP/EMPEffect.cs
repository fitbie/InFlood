using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    [CreateAssetMenu(fileName = "EMP", menuName = "InFlood/Effects/Status/EMP")]
    public class EMPEffect : StatusEffect
    {
        public override void ApplyEffect(object source, object target)
        {
            if (target is IEMPSensitive eMPSensitive and IEffectable effectable)
            {
                EMPStatusEffectData effectData = new(this, Duration, Delay, eMPSensitive);
                effectable.EffectsHandler.HandleEffect(effectData);
            }
        }
    }

}