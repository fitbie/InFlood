using InFlood.Entities.ActionSystem;
using InFlood.InputSystem;
using UnityEngine;

namespace InFlood.Entities.Modules
{
    [AddComponentMenu("Entities/Modules/Engine/EngineModule")]
    public class EngineModule : Module
    {
        [SerializeField] private float engineSpeed; // Speed increasing that Engine gives to ShipMoveController.MaxSpeed.
        private float currentEngineEffect = 0f; // Last engine effect was applied to ShipMoveController.MaxSpeed.
        private ShipMoveController ownerMoveController;


        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);
            if (owner is not IMoveable moveable) 
            { throw new InitializeModuleException
            ("You're trying to initialize EngineModule on non-IMoveable Entity!", this); }
                
            if (moveable.MoveController is not ShipMoveController shipMoveController)
            { throw new InitializeModuleException
            ("To use EngineModule you need to specify ShipMoveController type component in IMoveable.MoveController field!", this); }
            
            ownerMoveController = shipMoveController;
            ownerMoveController.OnMoveModeChanged += DeployEngine;
            DeployEngine(ownerMoveController.ShipMoveMode);
        }


        private void DeployEngine(ShipMoveMode moveMode)
        {
            float engineEffect = moveMode is ShipMoveMode.Engine ? engineSpeed : 0;
            ApplyEngineEffect(engineEffect);
        }


        private void ApplyEngineEffect(float newEngineEffect)
        {
            float delta = newEngineEffect - currentEngineEffect;
            ownerMoveController.ChangeMaxSpeed(delta);
            currentEngineEffect = newEngineEffect;
        }


        public override void Terminate()
        {
            base.Terminate();

            ownerMoveController.OnMoveModeChanged -= DeployEngine;
            ApplyEngineEffect(0);
        }


    }

}