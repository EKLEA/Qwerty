using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public UIInventory equippedGrid;
    public UIInventory �ollectableItemsGrid;
    public UIInventory �raftComponentsItemsGrid;
    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;
    private void Start()
    {
        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        �ollectableItemsGrid.SetupInvntoryUI(playerInventory.collectableItems);
        �raftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);
    }
}

