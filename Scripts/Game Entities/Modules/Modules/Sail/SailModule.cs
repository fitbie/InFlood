using InFlood.Entities.ActionSystem;
using InFlood.Global.Weather;
using InFlood.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Entities.Modules
{
    /// <summary>
    /// Provides sail behaviour, e.g. watching wind & rotate sails.
    /// </summary>
    [AddComponentMenu("Entities/Modules/Sail/SailModule")]
    public class SailModule : Module
    {
        /// <summary>
        /// What need to be rotated, e.g. mast with sails.
        /// </summary>
        [field: SerializeField] public Transform RotatablePart { get; private set; }
        private WindController windController;
        private ShipMoveController ownerMoveController;


        [Tooltip("Base speed that SailModule gives to ship without wind")]
        [SerializeField] private float baseSailSpeed = 1f;

        [Tooltip("Maximum speed that SailModule gives to ship when WindController.CurrentForce is 1")]
        [SerializeField] private float maxSailSpeed = 1f;

        
        // Is sail disabled? For EMP damage and stuff.
        private bool disabled = true;

        // Is sail currently in using? E.g. if ShipMoveMode is Engine or Default - it's false.
        private bool deployed = false;
        [SerializeField] private UnityEvent OnSailDeploy; // For sail deploy animation & stuff.
        [SerializeField] private UnityEvent OnSailUnDeploy; // For sail undeploy animation & stuff.

        private float sailEffect = 0; // Speed effect from sail+wind, applies to ownerMoveController max speed.

        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);
            if (owner is not IMoveable moveable) 
            { throw new InitializeModuleException("You're trying to initialize SailModule on non-Moveable Entity!", this); }
            
            if (moveable.MoveController is not ShipMoveController shipMoveController)
            { throw new InitializeModuleException
            ("To use SailModule you need to specify ShipMoveController type component in IMoveable.MoveController field!", this); }

            windController = WindController.Instance;
            disabled = false;
            
            ownerMoveController = shipMoveController; 
            ownerMoveController.OnMoveModeChanged += DeploySail;
            DeploySail(ownerMoveController.ShipMoveMode);
        }


        private void DeploySail(ShipMoveMode moveMode)
        {
            if (moveMode != ShipMoveMode.Default)
            {
                deployed = true;
                OnSailDeploy?.Invoke();
            }
            else
            {
                deployed = false;

                ApplySailEffect(0);

                OnSailUnDeploy?.Invoke();
            }
        }


        private void Update()
        {
            if (disabled || !deployed) { return; }
            
            // Rotate sail to "catch" wind, according to the "sailors theory"
            float angleToShip = Vector3.SignedAngle(-Owner.transform.forward, windController.CurrentDirection, Vector3.up);

            RotatablePart.localRotation = Quaternion.Euler(0, angleToShip / 2, 0);

            float newSailEffect = 
            baseSailSpeed + (maxSailSpeed - baseSailSpeed) * windController.CurrentForce * Mathf.Abs(angleToShip/180);
            newSailEffect = System.MathF.Round(newSailEffect, 2, System.MidpointRounding.AwayFromZero);
            ApplySailEffect(newSailEffect);
        }


        private void ApplySailEffect(float newSailEffect)
        {
            var delta = newSailEffect - sailEffect;
            ownerMoveController.ChangeMaxSpeed(delta);
            sailEffect = newSailEffect;
        }


        public override void Terminate()
        {
            disabled = true;
            ApplySailEffect(0);

            ownerMoveController.OnMoveModeChanged -= DeploySail;
            OnSailUnDeploy?.Invoke();

            base.Terminate();
        }
    }

}