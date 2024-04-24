using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInventoryController : UIInventoryScreen
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public UIInventory equippedGrid;
    public UIInventory ñollectableItemsGrid;
    public UIInventory ñraftComponentsItemsGrid;
    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;

    public EquippedMenuController equippementGrid;
    public override void InitUISceen()
    {

        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        equippementGrid.InitEquippedMenu();
        ñollectableItemsGrid.SetupInvntoryUI(playerInventory.collectableItems);
        ñraftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);
        PlayerInventory.Instance.blockInv = true;
        PlayerInventory.Instance.BlockPlayerInv();

        InventorySlot[] slots = playerInventory. weaponAndPerks.GetAllSlots();
        switch (PlayerController.Instance.playerLevelList.levelTier)
        {
            case 0:
                slots[1].isBlock = true;
                slots[2].isBlock = true;
                return;
            case 1:
                slots[1].isBlock = false;
                slots[2].isBlock = true;
                return;
            case 2:
                slots[1].isBlock = false;
                slots[2].isBlock = false;
                return;
        }
    }
}

