using System;
using UnityEngine;

namespace InFlood.InputSystem
{

/// <summary>
/// Base class providing fire input listening for ships. NPC / Player FireInputs derive from this.
/// </summary>
public abstract class FireInput : MonoBehaviour
{
    public event Action<bool> OnFireEvent;
    public event Action<Vector2> OnRotateEvent;

    protected void OnFireEventInvoke(bool value)
    {
        OnFireEvent?.Invoke(value);
    }


    protected void OnRotateEventInvoke(Vector2 direction)
    {
        OnRotateEvent?.Invoke(direction);
    }
    
}

}