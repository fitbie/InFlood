using InFlood.Entities.ActionSystem;


namespace InFlood.Entities.ActionSystem
{
/// <summary>
/// Provides entity moving withj MoveController using MoveInput.
/// </summary>
public interface IMoveable
{
    public MoveController MoveController { get; set; } // TODO: change namespaces
}

}