using Unity.Mathematics;
using UnityEngine;

namespace InFlood.Utils
{
    /// <summary>
    /// Simple inspector-helper class with public method that instantiates prefabs.
    /// </summary>
    public class PrefabInstantiator : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;


        public void Instantiate(Transform spawnPoint)
        {
            Instantiate(spawnPoint.position);
        }


        public void Instantiate(Vector3 position)
        {
            for (int i = 0; i < prefabs.Length; i++)
            {
                Instantiate(position, i);
            }
        }

        
        public void Instantiate(Vector3 position, int index)
        {
            Instantiate(prefabs[index], position, quaternion.identity);
        }
    }

}