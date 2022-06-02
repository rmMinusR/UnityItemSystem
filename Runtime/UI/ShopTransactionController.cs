using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ViewItemStack))]
public sealed class ShopTransactionController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isBuy;

    [Space]
    [SerializeField] private ItemInventory playerInv;
    private ViewItemStack view;
    private ItemInventory shopInv;
    [SerializeField] private Item currencyItem;

    [Space]
    [SerializeField] private UnityEvent onLMB;
    [SerializeField] private UnityEvent onRMB;

    private void Start()
    {
        view = GetComponent<ViewItemStack>();
        shopInv = view.inventory != null ? view.inventory : view.GetComponentInParent<ViewItemInventory>().inventory;
        if (playerInv == null) playerInv = GameObject.FindWithTag("Player").GetComponent<ItemInventory>();
        Debug.Assert(playerInv != null);
    }

    public void TryBuy(int quantity)
    {
        Transaction transaction = new Transaction(playerInv, new ItemStack[] { new ItemStack(currencyItem , view.itemType.buyPrice) },
                                                  shopInv,   new ItemStack[] { new ItemStack(view.itemType, 1                     ) });
        transaction = transaction.CloneAndMultiply(quantity);

        if (transaction.IsValid())
        {
            transaction.DoExchange();
        }
    }

    public void TrySell(int quantity)
    {
        Transaction transaction = new Transaction(playerInv, new ItemStack[] { new ItemStack(view.itemType, 1                      ) },
                                                  shopInv,   new ItemStack[] { new ItemStack(currencyItem , view.itemType.sellPrice) });
        transaction = transaction.CloneAndMultiply(quantity);

        if (transaction.IsValid())
        {
            transaction.DoExchange();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
             if (eventData.button == PointerEventData.InputButton.Left ) onLMB.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right) onRMB.Invoke();
    }
}
