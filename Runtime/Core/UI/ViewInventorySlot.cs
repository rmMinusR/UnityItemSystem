using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace rmMinusR.ItemAnvil.UI
{

    public sealed class ViewInventorySlot : Selectable
    {
        [SerializeField] private ItemStackViewCommon rendering;

        public InventoryHolder inventoryHolder { get; internal set; }
        
        private int slotID = -1;
        public InventorySlot slot => (0 <= slotID && slotID < inventoryHolder.inventory.SlotCount) ? inventoryHolder.inventory.GetSlot(slotID) : null;
        
        public void WriteSlot(int slotID)
        {
            this.slotID = slotID;
        }

        private void Update()
        {
            if (slot != null && !slot.IsEmpty)
            {
                //Has data, show
                rendering.WriteCount(slot.Contents.quantity);
                rendering.WriteType(slot.Contents.itemType);
            }
            else
            {
                //No data, show blank
                rendering.WriteCount("");
                rendering.WriteIcon(null);
            }
        }

        public bool IsSelected => EventSystem.current.currentSelectedGameObject == gameObject;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            ScrollTo();
            if (TryGetComponent(out Animator animator)) animator.SetBool("isCurrentSelection", true);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            if (TryGetComponent(out Animator animator)) animator.SetBool("isCurrentSelection", false);
        }

        public void ScrollTo() => GetComponentInParent<ViewInventory>().ScrollTo(this);
    }

}