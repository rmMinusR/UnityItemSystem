﻿using System.Collections;
using TMPro;
using rmMinusR.Tooltips;
using UnityEditor;
using UnityEngine;
using rmMinusR.ItemAnvil.UI;

namespace rmMinusR.ItemAnvil.Tooltips
{

    public sealed class TooltipItemStackBlurb : TooltipPart
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TMP_Text text;

        private ViewInventorySlot dataSource;

        protected override void UpdateTarget(Tooltippable newTarget)
        {
            dataSource = newTarget.GetComponent<ViewInventorySlot>();
            root.SetActive(dataSource != null && !dataSource.slot.IsEmpty);

            //Render text if active
            if (root.activeSelf) text.text = dataSource.slot.Contents.itemType.displayTooltip;
        }
    }

}