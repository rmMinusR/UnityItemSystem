using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace rmMinusR.ItemAnvil.UI
{

    public sealed class ViewInventorySlot : BaseViewItemStack, IPointerClickHandler, ISelectHandler
    {
        public InventoryHolder inventoryHolder { get; internal set; }
        public InventorySlot slot { get; internal set; }

        public void WriteSlot(InventorySlot src)
        {
            slot = src;

            if (slot != null && !slot.IsEmpty)
            {
                //Has data, show
                WriteCount(slot.Contents.quantity);
                WriteType (slot.Contents.itemType);
            }
            else
            {
                //No data, show blank
                WriteCount("");
                WriteIcon(null);
            }
        }

        public void OnPointerClick(PointerEventData eventData) => EventSystem.current.SetSelectedGameObject(gameObject);
        public void OnSelect(BaseEventData eventData) => ScrollTo();

        public void ScrollTo() => GetComponentInParent<ViewInventory>().ScrollTo(this);
    }

}