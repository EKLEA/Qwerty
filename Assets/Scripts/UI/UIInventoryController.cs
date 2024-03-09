using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    public PlayerUseMoment playerUseMoment;

    public GameObject equippedGrid;
    public GameObject ñollectableItemsGrid;
    public GameObject ñraftComponentsItemsGrid;

    private UIInventorySlot[] uiEquippedSlots => equippedGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCollectableItemsSlots => ñollectableItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCraftComponentsIntemsSlots => ñraftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {
        equippedGrid.GetComponent<UIInventory>().inventory = playerInventory.equippedItems;
        equippedGrid.GetComponent<UIInventory>().slots = uiEquippedSlots;
        ñollectableItemsGrid.GetComponent<UIInventory>().inventory = playerInventory.collectableItems;
        ñollectableItemsGrid.GetComponent<UIInventory>().slots = uiCollectableItemsSlots;
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().inventory = playerInventory.craftComponents;
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().slots= uiCraftComponentsIntemsSlots;

    }
}

