using UnityEngine;

namespace InFlood.Entities
{

/// <summary>
/// Base class for all game entites, e.g. ships, monsters, etc..
/// </summary>
public abstract class Entity : MonoBehaviour
{
    public abstract void Initialize();
}

}