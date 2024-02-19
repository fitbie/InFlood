using System.Collections.Generic;
using InFlood.Entities.ActionSystem;
using UnityEngine;

namespace InFlood.Entities.Modules
{
    [AddComponentMenu("Entities/Modules/Protection/ProtectionModule")]
    public class ProtectionModule : Module
    {
        [field:SerializeField] public List<DamageResist> DamageResistsBuff { get; protected set; }


        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            ChangeResist(1);
        }


        public override void Terminate()
        {
            base.Terminate();

            ChangeResist(-1);
        }


        public void ChangeResist(float modifier)
        {
            if (Owner is IHaveResists resists)
            {
                foreach (var element in DamageResistsBuff)
                {
                    if (element.ResistValue == 0) { continue; }

                    resists.ChangeResist(element.DamageType, element.ResistValue * modifier);
                }
            }

            else 
            { throw new System.Exception("You're trying add ProtectionModule to Entity that does't implement IHaveResists interface!"); }
        }


    }

}