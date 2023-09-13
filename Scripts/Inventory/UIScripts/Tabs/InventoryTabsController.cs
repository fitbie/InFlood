using System;
using Inventory;
using UnityEngine;

public class InventoryTabsController : MonoBehaviour
{
    [field:SerializeField] public InventoryPanelBuilder InventoryPanel { get; private set; } // TODO getting acces.

    public enum TabType { OnBoard, Sell, Buy, Quest }
    private TabType currentTab;



    private void Awake()
    {
        InventoryPanel.Inventory.InventoryChangedEventHandler += UpdateTab;
    }


    private void OnEnable() // Show OnBoard on opening.
    {
        OpenOnBoardTab();    
    }


    private void UpdateTab(object _, EventArgs __)
    {
        switch (currentTab)
        {
            case TabType.OnBoard:
            OpenOnBoardTab();
            break;

            case TabType.Sell:
            OpenSellTab();
            break;

            case TabType.Buy:
            OpenBuyTab();
            break;

            case TabType.Quest:
            OpenQuestTab();
            break;
        }
    }


    public void OpenOnBoardTab()
    {
        OnBoardTab.ShowOnBoardSlots(InventoryPanel);
        currentTab = TabType.OnBoard;
    }


    public void OpenSellTab()
    {
        SellTab.ShowSellSlots(InventoryPanel);
        currentTab = TabType.Sell;
    }


    public void OpenBuyTab()
    {
        //TODO : Selling
        currentTab = TabType.Buy;
    }


    public void OpenQuestTab()
    {
        QuestTab.ShowQuestSlots(InventoryPanel);
        currentTab = TabType.Quest;
    }
}
