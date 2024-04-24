using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class UIStorageController : UIInventoryScreen
{


    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();

   
    public UIInventory ñraftComponentsItemsGrid;
    public UIInventory storageItemsGrid;

    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;
    public UIInventory equippedGrid;
    public EquippedMenuController equippementGrid;
    public override void InitUISceen()
    {

        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        equippementGrid.InitEquippedMenu();
        GetComponentInChildren<EquippedMenuController>().InitEquippedMenu();
        ñraftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);

        storageItemsGrid.SetupInvntoryUI(playerInventory.storageItems);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);
        PlayerInventory.Instance.blockInv = false;
        PlayerInventory.Instance.BlockPlayerInv();

        InventorySlot[] slots = playerInventory.weaponAndPerks.GetAllSlots();

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
