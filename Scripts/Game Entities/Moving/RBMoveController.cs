using System.Collections;
using System.Collections.Generic;
using InFlood.InputSystem;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{
    /// <summary>
    /// Version of MoveController, using RigidBody to move.
    /// </summary>
    public abstract class RBMoveController : MoveController
    {
        [field: SerializeField] public Rigidbody RB { get; protected set; }
        public override Vector3 CurrentVelocity { get => RB.velocity; set => RB.velocity = value; }
    }

}