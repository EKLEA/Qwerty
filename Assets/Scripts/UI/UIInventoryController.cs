using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject equippedGrid;
    public GameObject �ollectableItemsGrid;
    public GameObject �raftComponentsItemsGrid;
    private void Awake()
    {
        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems);
        �ollectableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.collectableItems);
        �raftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);
    }
}

