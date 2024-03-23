using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject equippedGrid;
    public GameObject ñollectableItemsGrid;
    public GameObject ñraftComponentsItemsGrid;

    private UIInventorySlot[] uiEquippedSlots => equippedGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCollectableItemsSlots => ñollectableItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uiCraftComponentsIntemsSlots => ñraftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private void OnEnable()
    {

        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems, uiEquippedSlots);
        ñollectableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.collectableItems, uiCollectableItemsSlots);
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents,uiCraftComponentsIntemsSlots);
    }
}

