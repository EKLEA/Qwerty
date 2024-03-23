using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject equippedGrid;
    public GameObject �ollectableItemsGrid;
    public GameObject �raftComponentsItemsGrid;

    private UIInventorySlot[] uiEquippedSlots => equippedGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCollectableItemsSlots => �ollectableItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCraftComponentsIntemsSlots => �raftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {

        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems, uiEquippedSlots);
        �ollectableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.collectableItems, uiCollectableItemsSlots);
        �raftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents,uiCraftComponentsIntemsSlots);
    }
}

