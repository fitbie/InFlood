using System;
using InventorySystem;
using UnityEngine;

public class InventoryTabsController : MonoBehaviour
{
    [field:SerializeField] public PlayerInventoryPanel PlayerInventoryPanel { get; set; } // Inspector.
    [SerializeField] private InventoryTab defaultTab; // Open this tab by default.
    public static InventoryTab currentTab { get; private set; }



    private void Start()
    {
        InventoryTab.OnTabClick += OpenTab;
        PlayerInventoryPanel.Inventory.InventoryChangedEventHandler += UpdateTab;
    }


    private void OnEnable() // Show OnBoard on opening.
    {
        OpenTab(defaultTab);
    }


    private void UpdateTab(object _, EventArgs __)
    {
        currentTab.OpenTab(); // Refresh tab by opening it again.
    }
    

    public void OpenTab(InventoryTab tab)
    {
        if (currentTab == tab) { return; }
        if (currentTab == null) {currentTab = tab; }
        
        currentTab.CloseTab();
        tab.OpenTab();
        currentTab = tab;
    }


    private void OnDestroy()
    {
        InventoryTab.OnTabClick -= OpenTab;
        PlayerInventoryPanel.Inventory.InventoryChangedEventHandler -= UpdateTab;    
    }

}
