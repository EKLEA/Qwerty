using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    public PlayerUseMoment playerUseMoment;

    public GameObject equippedGrid;
    public GameObject �ollectableItemsGrid;
    public GameObject �raftComponentsItemsGrid;

    private UIInventorySlot[] uiEquippedSlots => equippedGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCollectableItemsSlots => �ollectableItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCraftComponentsIntemsSlots => �raftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {
        equippedGrid.GetComponent<UIInventory>().inventory = playerInventory.equippedItems;
        equippedGrid.GetComponent<UIInventory>().slots = uiEquippedSlots;
        �ollectableItemsGrid.GetComponent<UIInventory>().inventory = playerInventory.collectableItems;
        �ollectableItemsGrid.GetComponent<UIInventory>().slots = uiCollectableItemsSlots;
        �raftComponentsItemsGrid.GetComponent<UIInventory>().inventory = playerInventory.craftComponents;
        �raftComponentsItemsGrid.GetComponent<UIInventory>().slots= uiCraftComponentsIntemsSlots;

    }
}

