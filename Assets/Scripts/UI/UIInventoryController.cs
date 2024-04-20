using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryController : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public UIInventory equippedGrid;
    public UIInventory �ollectableItemsGrid;
    public UIInventory �raftComponentsItemsGrid;
    public UIInventory weaponAndPerksGrid;
    public UIInventory abilitiesGrid;
    private void OnEnable()
    {

        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        �ollectableItemsGrid.SetupInvntoryUI(playerInventory.collectableItems);
        �raftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);

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

