using InFlood.InputSystem;
using UnityEngine;

namespace InFlood.Entities.ActionSystem
{

/// <summary>
/// Controls Entity's move behaviour.
/// </summary>
public abstract class MoveController : MonoBehaviour
{
    public abstract Vector3 CurrentVelocity { get; set; }
    [field:SerializeField] public float MaxSpeed { get; private set; }


    public virtual void ChangeMaxSpeed(float value)
    {
        MaxSpeed += value;
        if (MaxSpeed < 0) { MaxSpeed = 0; }
    }


    public abstract void Initialize(MoveInput input);
}

}