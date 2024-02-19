using UnityEngine;

namespace InFlood.InputSystem
{

/// <summary>
/// Base class providing move input logic for Game Entities. All specific inputs are derived from this class.
/// </summary>
public abstract class MoveInput : MonoBehaviour
{
    public Vector3 Direction { get; protected set; }
}

}