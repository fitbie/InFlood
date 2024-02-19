using System;
using System.Collections.Generic;
using System.Linq;
using InFlood.Entities.ActionSystem;
using InFlood.Entities.Modules;
using InFlood.InputSystem;
using InFlood.Utils;
using UnityEngine;

namespace InFlood.Entities
{

    /// <summary>
    /// Class, representing basic ship unit.
    /// </summary>
    [AddComponentMenu("Entities/Ships/Ship")]
    public class Ship : Entity, IHaveHealth, IDamageable, IHaveModules, IMoveable, IFireInput, IHaveResists, IEMPSensitive, IEffectable
    {
        #region Interfaces

        #region Health

        [SerializeField] private IHaveHealth.HealthData health;
        public IHaveHealth.HealthData Health 
        { 
            get => health;
            set
            {
                health = value;
                if (health.CurrentHealth <= 0) { OnDieEvent?.Invoke(); }
                if (health.CurrentHealth > health.MaxHealth) { health.CurrentHealth = health.MaxHealth; }
            }
        }
        
        public event Action OnDieEvent;
        public event Action<float> OnHealthChanged;

        #endregion

        #region Damageable

        public void TakeDamage(DamageType damageType, float value)
        {
            var resist = (IHaveResists)this;
            var damage = value - value * resist.GetResist(damageType);
            ((IHaveHealth)this).ChangeHealth(-damage);
            OnHealthChanged?.Invoke(damage);
        }

        #endregion
        
        #region Resists

        [field: SerializeField] public List<DamageResist> DamageResists { get; set; }

        #endregion

        #region Modules
        [field: SerializeField] public ModulesManager ModulesManager { get; set; }

        #endregion

        #region Moveable

        /// <summary>
        /// MoveController of this Ship. Specify only ShipMoveController.cs component to this field! 
        /// </summary>
        [field: SerializeField] public MoveController MoveController { get; set; }

        #endregion

        #region FireInput

        /// <summary>
        /// Fire input, used by WeaponModules. Ships dont have their own FireInput, 
        /// instead it passed by Player's or AI's IFireInput in Initialize().
        /// </summary>
        [field: SerializeField] public FireInput FireInput { get; set; }

        #endregion
            
        #region EMPSensitive

        private ModuleSlot[] disabledSlots;

        // In Ship's case after EMP is applied we disable random amount of Ship's Modules.
        public void ApplyEMP(bool status)
        {
            if (status)
            {
                if (disabledSlots == null)
                {
                    // Get slots with setted module.
                    var settedSlots = ModulesManager.ModuleSlots.Where((slot) => slot.Module != null).ToArray();

                    // Shuffle them for fun.
                    System.Random rnd = new();
                    rnd.ShuffleArray(settedSlots);

                    int amountToOff = UnityEngine.Random.Range(0, settedSlots.Length);
                    disabledSlots = new ModuleSlot[amountToOff];

                    for (int i = 0; i < amountToOff; i++)
                    {
                        settedSlots[i].Module.Terminate();
                        disabledSlots[i] = settedSlots[i];
                    }

                }
            }
            else if (disabledSlots != null)
            {
                foreach (var slot in disabledSlots)
                {
                    slot.Initialize(this);
                }

                disabledSlots = null;
            }
        }

        #endregion

        #region IEffectable

        [field: SerializeField] public StatusEffectsHandler EffectsHandler { get; set; }

        #endregion

        #endregion


        [Serializable]
        public struct CargoCapacityData
        {
            [field: SerializeField] public float CurrentCapacity { get; private set; }
            [field: SerializeField] public float MaxCapacity { get; private set; }
            public bool TryChangeCargo(float value)
            {
                if (CurrentCapacity + value > MaxCapacity) { return false; }
                CurrentCapacity += value;
                if (CurrentCapacity < 0) 
                {
                    CurrentCapacity = 0;
                    Debug.LogWarning($"New ShipCargoCapacity is below zero! Consider fix for this.");
                }

                return true;
            }
        }
        /// <summary>
        /// Provides reading max ship's capacity, adding/removing cargo.
        /// </summary>
        /// <value></value>
        [field: SerializeField] public CargoCapacityData ShipCargoCapacity { get; private set; }


        private void Start() // TODO: Delete this.
        {
            Initialize();    
        }

        public override void Initialize(/*ShipMoveInput moveInput, FireInput fireInput*/)
        {
            // TODO: Full initialization
            
            // MoveController.Initialize(moveInput);
            // FireInput = fireInput;

            ModulesManager = ModulesManager == null ? GetComponentInChildren<ModulesManager>() : ModulesManager;
            ModulesManager.Initialize(this);

        }

    }

}
