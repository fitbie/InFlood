using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{

using DisplayMode = InventoryUISlot.DisplayMode;

[RequireComponent(typeof(InventoryUISlot))]
public class ClickOnInventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryUISlotPanel slotPanelPrefab;
    private InventoryUISlot uiSlot;

    void Start()
    {
        uiSlot = GetComponent<InventoryUISlot>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ClickResponce(uiSlot.CurrentDisplayMode);
    }


    public void ClickResponce(DisplayMode displayMode)
    {
        switch (displayMode)
        {
            case DisplayMode.Default:
            var slotPanel = Instantiate(slotPanelPrefab, transform.parent.parent);
            slotPanel.BuildPanel(uiSlot.SlotData.Item);
            break;

            case DisplayMode.Price:
            Debug.Log("Sell");
            break;
        }
    }
}

}
