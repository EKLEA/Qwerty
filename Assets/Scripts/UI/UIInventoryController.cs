using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject equippedGrid;
    public GameObject ñollectableItemsGrid;
    public GameObject ñraftComponentsItemsGrid;
    private void Awake()
    {
        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems);
        ñollectableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.collectableItems);
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);
    }
}

