using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Binds a CraftingRecipe to a Component, and makes it callable by UI
/// </summary>
public sealed class CallableCraftingRecipe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CraftingRecipe recipe;

    [Space]
    [SerializeField] private UnityEvent onLMB;
    [SerializeField] private UnityEvent onRMB;

    public void TryCraftAsHolder(InventoryHolder holder) => TryCraft(holder.inventory);

    public void TryCraft(Inventory crafter) => TryCraft(crafter, 1);

    public void TryCraft(Inventory crafter, int count)
    {
        if(!recipe.TryExchange(crafter, count))
        {
            Debug.LogError("Crafting not performed - inputs not met");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
             if (eventData.button == PointerEventData.InputButton.Left ) onLMB.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right) onRMB.Invoke();
    }
}
