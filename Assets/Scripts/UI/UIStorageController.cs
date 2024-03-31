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
    private void Start()
    {

        equippedGrid.SetupInvntoryUI(playerInventory.equippedItems);
        ñraftComponentsItemsGrid.SetupInvntoryUI(playerInventory.craftComponents);

        storageItemsGrid.SetupInvntoryUI(playerInventory.storageItems);
        weaponAndPerksGrid.SetupInvntoryUI(playerInventory.weaponAndPerks);
        abilitiesGrid.SetupInvntoryUI(playerInventory.abilities);
    }
   
}
