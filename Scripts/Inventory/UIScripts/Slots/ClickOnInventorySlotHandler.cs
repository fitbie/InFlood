using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{

using SlotState = InventoryUISlot.SlotState;

/// <summary>
///  Handle some logic wehen user clicks on inventory slot.
/// </summary>
[RequireComponent(typeof(InventoryUISlot))]
public class ClickOnInventorySlotHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryUISlotPanel slotPanelPrefab; // Info panel that opens when we click on slot.
    protected InventoryUISlot uiSlot;
    protected Merchant merchant;



    protected virtual void Start()
    {
        Merchant.OnMerchantTriggered += SetMerchant;

        uiSlot = GetComponent<InventoryUISlot>();
    }


    private void SetMerchant(Merchant currentMerchant)
    {
        merchant = currentMerchant.IsTriggered ? currentMerchant : null;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ClickResponce(uiSlot.CurrentDisplayMode);
    }


    private void ClickResponce(SlotState slotState)
    {
        switch (slotState)
        {
            case SlotState.Default:
            DefaultInteract();
            break;

            case SlotState.Price:
            PriceInteract();
            break;
        }
    }

    protected virtual void DefaultInteract()
    {
        var slotPanel = Instantiate(slotPanelPrefab, transform.parent.parent);
        slotPanel.BuildPanel(uiSlot.SlotData.Item);
    }


    protected virtual void PriceInteract()
    {
        Merchant.Current.SellItem(uiSlot.SlotData);
    }
}

}


