using System;
using InFlood.InputSystem;

/// <summary>
/// Base class for Player & AI ShipMoveInputs.
/// </summary>
public abstract class ShipMoveInput : MoveInput
{
    public event Action<int> OnMoveModeChanged;


    public virtual void ChangeMoveMode(int value)
    {
        OnMoveModeChanged?.Invoke(value);
    }
}
