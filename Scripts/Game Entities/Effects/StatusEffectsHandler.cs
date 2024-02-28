using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Handles status (temporary) effects. Contain StatusEffectData HashSet, invokes StatusEffectData.Tick() every
    /// StatusEffectData.Delay seconds, revert expired effects and send OnEffectDataChanged events.
    /// </summary>
    public class StatusEffectsHandler : MonoBehaviour
    {
        /// <summary>
        /// Currently handling effects.
        /// </summary>
        public HashSet<StatusEffectData> CurrentEffects { get; private set; } = new();

        /// <summary>
        /// Raised when handle start handling new StatusEffectData.
        /// </summary>
        public event Action<StatusEffectData> OnEffectApplied;

        /// <summary>
        /// Raised when any StatusEffectData changed, e.g. CurrentTime timer or Concatenate new effect to existing.
        /// </summary>
        public event Action<StatusEffectData> OnEffectChanged;

        /// <summary>
        /// Raised when effect's lifetime is over, or canceled.
        /// </summary>
        public event Action<StatusEffectData> OnEffectReverted;

        /// <summary>
        /// Pass here StatusEffectData instance to starts its handling process.
        /// If there is no Equal() effect in CurrentEffects - add new and start handle timer. If there is -
        /// Concatenates containing and new effect into containing, e.g. prolongs its timer and other logic.
        /// </summary>
        /// <param name="effect"></param>
        public void HandleEffect(StatusEffectData effect)
        {
            if (!CurrentEffects.TryGetValue(effect, out var contained)) 
            { 
                CurrentEffects.Add(effect);
                OnEffectApplied?.Invoke(effect);
                StartCoroutine(EffectHandleRoutine(effect));
            }
            else 
            { 
                contained.Concat(effect);
                OnEffectChanged?.Invoke(contained); 
            }
        }

        /// <summary>
        /// Counts effect's data timer and invokes Tick(). When CurrentTime is over - removes effect from HashSet and
        /// send OnEffectReverted.
        /// </summary>
        private IEnumerator EffectHandleRoutine(StatusEffectData effect)
        {
            yield return null; // Skip first Tick() when effect just been applied.

            while (effect.CurrentTime > 0)
            {
                effect.Tick();
                OnEffectChanged?.Invoke(effect);
                effect.CurrentTime -= effect.Delay;
                yield return new WaitForSeconds(effect.Delay);
            }

            effect.RevertEffect();
            OnEffectReverted?.Invoke(effect);

            CurrentEffects.Remove(effect);
        }
    }

}