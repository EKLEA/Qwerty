using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class UIStorageController : MonoBehaviour
{


    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();

   
    public UIInventory ñraftComponentsItemsGrid;
    public UIInventory storageItemsGrid;

    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;
    public UIInventory equippedGrid;
    private void OnEnable()
    {

        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        ñraftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);

        storageItemsGrid.SetupInvntoryUI(playerInventory.storageItems);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);

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
