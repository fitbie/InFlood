using System.Collections;
using UnityEngine;


namespace Quests
{

[AddComponentMenu("Quests/QuestPointerController")]
/// <summary>
/// This class rotates pointer around player and make it points to current target.
/// </summary>
public class QuestPointerController : MonoBehaviour
{
    public Transform currentTarget { get; private set; } // Current destination
    private Coroutine rotationRoutine;

    [SerializeField] private Transform pointer; // Sprite rotating towards QuestMark.



    /// <summary>
    /// New target for pointer.
    /// </summary>
    /// <param name="destination">Where pointer should point now.</param>
    public void SetPointerTarget(Transform destination)
    {
        StopRotationRoutine();

        if (destination == null) { throw new System.Exception("QuestPointerRotator destination mark equals null!"); }
        
        if (!pointer.gameObject.activeSelf) { pointer.gameObject.SetActive(true); }

        currentTarget = destination;

        rotationRoutine = StartCoroutine(RotationRoutine());
    }


    public void StopPointer()
    {
        StopRotationRoutine();

        pointer.gameObject.SetActive(false);
    }


    private void StopRotationRoutine()
    {
        if (rotationRoutine != null) 
        { 
            StopCoroutine(rotationRoutine);
            rotationRoutine = null; 
        }
    }


    private IEnumerator RotationRoutine()
    {
        while (true)
        {
            var direction = new Vector3(currentTarget.position.x, pointer.position.y, currentTarget.position.z);
            pointer.transform.LookAt(direction);

            yield return null;
        }
    }
}

}
