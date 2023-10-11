using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{

/// <summary>
/// Base class for inventory tabs. All tabs require UI Button component.
/// </summary>
[RequireComponent(typeof(Button))] 
public abstract class InventoryTab : MonoBehaviour
{
    public static event Action<InventoryTab> OnTabClick;
    protected Button button;


    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnTabClick?.Invoke(this));
    }

    public abstract void OpenTab();


    public abstract void CloseTab();
    
}

}
