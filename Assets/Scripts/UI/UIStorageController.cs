using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStorageController : MonoBehaviour
{

    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject equippedGrid;
    public GameObject �raftComponentsItemsGrid;
    public GameObject storageItemsGrid;

    private UIInventorySlot[] uiEquippedSlots => equippedGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCraftComponentsIntemsSlots => �raftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiStorageItemsSlots => storageItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {

        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems, uiEquippedSlots);
        �raftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents, uiCraftComponentsIntemsSlots);
        storageItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.storageItems, uiStorageItemsSlots);
    }
}
