using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public sealed class CallableMarketTransaction : MonoBehaviour, IPointerClickHandler
{
    [Space]
    [SerializeField] private InventoryHolder inventoryA;
    [SerializeField] private InventoryHolder inventoryB;
    private ViewItemStack view;
    [SerializeField] private Item itemType;
    [SerializeField] private Item currencyItem;

    [Space]
    [SerializeField] private UnityEvent onLMB;
    [SerializeField] private UnityEvent onRMB;

    private void Start()
    {
        view = GetComponent<ViewItemStack>();
        if (view != null) itemType = view.itemType;
        if (inventoryA == null) inventoryA = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>();
        if (inventoryB == null) inventoryB = view.inventoryHolder != null ? view.inventoryHolder : view.GetComponentInParent<ViewInventory>().inventoryHolder;
        Debug.Assert(inventoryA != null);
    }

    private void Update()
    {
        if (view != null) itemType = view.itemType;
    }

    public void TryBuy(int quantity)
    {
        Marketable m = itemType.properties.Get<Marketable>();
        if (m == null)
        {
            Debug.LogError(itemType + " has no property 'Marketable'", this);
            return;
        }
        else if (!m.isBuyable)
        {
#if UNITY_EDITOR
            Debug.Log(itemType + " cannot be bought", this);
#endif
            return;
        }

        Transaction transaction = new Transaction(new ItemStack[] { new ItemStack(currencyItem, m.buyPrice) },
                                                  new ItemStack[] { new ItemStack(itemType    , 1         ) });
        transaction.MultiplyInPlace(quantity);
        TryPerformTransaction(transaction);
    }

    public void TrySell(int quantity)
    {
        Marketable m = itemType.properties.Get<Marketable>();
        if (m == null)
        {
            Debug.LogError(itemType + " has no property 'Marketable'", this);
            return;
        }
        else if (!m.isSellable)
        {
#if UNITY_EDITOR
            Debug.Log(itemType + " cannot be sold", this);
#endif
            return;
        }

        Transaction transaction = new Transaction(new ItemStack[] { new ItemStack(itemType    , 1          ) },
                                                  new ItemStack[] { new ItemStack(currencyItem, m.sellPrice) });
        transaction.MultiplyInPlace(quantity);
        TryPerformTransaction(transaction);
    }

    public void TryPerformTransaction(Transaction transaction)
    {
        Debug.Log("A= "+inventoryA+" B= "+inventoryB);
        transaction.Log();
        if(!transaction.TryExchange(inventoryA.inventory, inventoryB.inventory))
        {
            Debug.LogError("Failed - conditions not met");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
             if (eventData.button == PointerEventData.InputButton.Left ) onLMB.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right) onRMB.Invoke();
    }
}