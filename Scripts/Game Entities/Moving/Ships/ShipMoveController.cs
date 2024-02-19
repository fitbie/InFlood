using System;
using InFlood.InputSystem;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{

    public enum ShipMoveMode { Default, Sail, Engine }

    [AddComponentMenu("Entities/Moving/ShipMoveController")]
    /// <summary>
    /// Controls Ship's Move Behaviour, using Player/AI MoveInput.
    /// </summary>
    public class ShipMoveController : RBMoveController
    {
        [field: SerializeField] public float RotationSpeed { get; protected set; }
        [SerializeField] private ShipMoveMode shipMoveMode;
        public ShipMoveMode ShipMoveMode
        {
            get => shipMoveMode;
            set
            {
                shipMoveMode = value;
                OnMoveModeChanged?.Invoke(value);
            }
        }
        public event Action<ShipMoveMode> OnMoveModeChanged;

        public ForceMode forceMode;
        public ShipMoveInput moveInput;


        private void Start() { // TODO: DELETE
            Initialize(moveInput);
        }


        public override void Initialize(MoveInput input)
        {
            if (moveInput is not ShipMoveInput shipInput)
            { throw new Exception("You're trying to initialize ShipMoveController with not ShipMoveInput input type!"); }
            moveInput = shipInput;
            moveInput.OnMoveModeChanged += ChangeMoveMode;
        }


        public void ChangeMoveMode(int value)
        {
            ShipMoveMode = (ShipMoveMode)((value + (int)ShipMoveMode + 3) % 3);
        }


        public void RotateShip()
        {
            var direction = moveInput.Direction;
            if (direction == Vector3.zero) { return; }

            float targetAngle = Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;
            targetAngle = (targetAngle + 360) % 360;

            float currentAngle = (RB.rotation.eulerAngles.y + 360) % 360;

            float delta = Mathf.DeltaAngle(currentAngle, targetAngle) * Time.fixedDeltaTime * RotationSpeed;
            RB.rotation = Quaternion.Euler(0, currentAngle + delta, 0);
        }


        private void FixedUpdate()
        {
            RB.AddForce(RB.transform.forward * MaxSpeed, forceMode);
            
            RotateShip();
            
        }
    }

}