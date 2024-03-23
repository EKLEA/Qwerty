using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenuScript : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    
    public GameObject �raftComponentsItemsGrid;
    public GameObject craftableItemsGrid;

    private UIInventorySlot[] uiCraftComponentsIntemsSlots => �raftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uicraftableItemsSlots => craftableItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {
        �raftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents, uiCraftComponentsIntemsSlots);
        //craftableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftableItems, uicraftableItemsSlots);
    }
}
