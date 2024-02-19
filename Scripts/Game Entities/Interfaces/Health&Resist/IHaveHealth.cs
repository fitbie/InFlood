using System;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Provides implementing entity health logic, i.e. max and current Health props, method to change health
    /// </summary>
    public interface IHaveHealth
    {
        [Serializable]
        public struct HealthData
        {
            [field: SerializeField] public float MaxHealth { get; private set; }
            [field: SerializeField] public float CurrentHealth { get; set; }
        }

        public HealthData Health { get; set; }
        
        public void ChangeHealth(float value)
        {
            var temp = Health;
            temp.CurrentHealth += value;
            Health = temp;
        }

        public event Action OnDieEvent;
        public event Action<float> OnHealthChanged;
        
    }

}