using UnityEngine;
using UnityEngine.Events;
using InventorySystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InventorySystem
{

/// <summary>
/// Scene class for adding item.
/// </summary>
[AddComponentMenu("Inventory/Add Inventory Item")]
public class AddInventoryItem : MonoBehaviour
{
    [SerializeField] private bool addToPlayerInventory = true;
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventorySlot[] itemsToAdd;
    public GameObject prefabToInstantiate;
    [SerializeField] private UnityEvent onAdd;
    void Start(){
        prefabToInstantiate = itemsToAdd[0].Item.ContainerPrefab;
    }

    // method to change items to add


    public void AddItems()
    {
        inventory = addToPlayerInventory ? GameManager.Instance.Player.Inventory : inventory;

        if (itemsToAdd.Length == 0) { return; }
        foreach (var item in itemsToAdd)
        {
            inventory.AddItem(item.Item, item.Amount);
        }

        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/PickUpItem", transform.position);

        onAdd?.Invoke();
    }

    public void SetItems(InventorySlot[] newItemsToAdd){
        itemsToAdd = newItemsToAdd;
        GetComponentInChildren<LootCargoVisualSpawner>().ChangeLootPrefab(itemsToAdd[0].Item.ContainerPrefab);
    }

}

}

///----------------------------------------------------------------
/// Editor Class
///----------------------------------------------------------------
 
#if UNITY_EDITOR
[CustomEditor(typeof(AddInventoryItem))]
public class AddInventoryItemEditor : Editor
{
    //All serialized properties
   #region SerializedProperties

    SerializedProperty addToPlayerInventory;
    SerializedProperty inventory;
    SerializedProperty prefabToInstantiate;
    SerializedProperty itemsToAdd;
    SerializedProperty onAdd;
    
   #endregion


   private void OnEnable() //Find serialized properties and cash them
   {
        addToPlayerInventory = serializedObject.FindProperty("addToPlayerInventory");
        inventory = serializedObject.FindProperty("inventory");

         itemsToAdd = serializedObject.FindProperty("itemsToAdd");
         onAdd = serializedObject.FindProperty("onAdd");
   }

   public override void OnInspectorGUI() 
   {
        serializedObject.Update();

        EditorGUILayout.PropertyField(addToPlayerInventory);
        if (!addToPlayerInventory.boolValue)
        {
            EditorGUILayout.PropertyField(inventory);
        }

        EditorGUILayout.Space(5); //Space between lines

        EditorGUILayout.PropertyField(itemsToAdd);

        EditorGUILayout.Space(5);

        EditorGUILayout.PropertyField(onAdd);

        serializedObject.ApplyModifiedProperties();
   }
}
#endif
