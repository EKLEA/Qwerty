using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public UIInventory equippedGrid;
    public UIInventory ñollectableItemsGrid;
    public UIInventory ñraftComponentsItemsGrid;
    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;
    private void Start()
    {
        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        ñollectableItemsGrid.SetupInvntoryUI(playerInventory.collectableItems);
        ñraftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);
    }
}

