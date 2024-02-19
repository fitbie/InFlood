using UnityEngine;
using UnityEngine.Events;

namespace InFlood.Utils
{

public class DestroyGameObject : MonoBehaviour
{
    [Tooltip("If null - destroy this GameObjecy")]
    [SerializeField] private GameObject objectToDestroy; // Otional

    [Tooltip("Wait n seconds before destroying GameObject")]
    [SerializeField] private float delay = 0f;
    [SerializeField] private UnityEvent onDestroy;


    public void Destroy()
    {
        GameObject GO = objectToDestroy == null ? gameObject : objectToDestroy;
        Destroy(GO, delay);
    }


    private void OnDestroy()
    {   
        onDestroy?.Invoke();
    }
}

}