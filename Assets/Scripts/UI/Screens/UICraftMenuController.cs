using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class UICraftMenuScript : UIInventoryScreen
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject сraftComponentsItemsGrid;
    public GameObject craftableItemsGrid;
    public GameObject button;
    public GameObject SlotPrefab;
    public GameObject requieCraftComponentsGrid;
    public GameObject itemSlot;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textHaract;
    public TextMeshProUGUI textDesc;
    InventoryWithSlots requieCraftComponents = new InventoryWithSlots(3, SlotTypes.StaticSlot, InventoryType.Storage);
    UnityEngine.UI.Button bt;
    public override void InitUISceen()
    {
        bt = button.GetComponent<UnityEngine.UI.Button>();
        button.SetActive(false);
        сraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);
        if (craftableItemsGrid.transform.childCount ==0)
        {
            for (int i = 0; i < playerInventory.craftableItems.GetAllSlots().Length; i++)
            {
                GameObject temp = Instantiate(SlotPrefab);

                temp.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() => OnCraftItemSelected(temp.GetComponentInChildren<UnityEngine.UI.Button>()));

                temp.GetComponent<RectTransform>().SetParent(craftableItemsGrid.transform, false);
            }
        }
        craftableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftableItems);

        InventorySlot[] slots = requieCraftComponents.GetAllSlots();

        slots[0].requieItem = ItemBase.ItemsInfo["Bolts"];
        slots[1].requieItem = ItemBase.ItemsInfo["Fluid"];
        slots[2].requieItem = ItemBase.ItemsInfo["Electronics"];



    }
    private void OnEnable()
    {
        craftableItemsGrid.transform.localPosition = new Vector3(0, -1630f, 0f);
    }
    public void OnButtonClick(UnityEngine.UI.Button tempBt)
    {
        InventorySlot slot = tempBt.GetComponentInParent<UICraftSlot>().slot;
        playerInventory.storageItems.TryToAdd(null,slot.item) ;
        tempBt.GetComponentInParent<UICraftSlot>().Refresh();

        playerInventory.craftComponents.Remove(null, "Bolts", (slot.item.info as CraftableItemInfo).requeBolts);
        playerInventory.craftComponents.Remove(null, "Fluid", (slot.item.info as CraftableItemInfo).requeFluid);
        playerInventory.craftComponents.Remove(null, "Electronics", (slot.item.info as CraftableItemInfo).requeElectronics);


        
        bt.interactable = false;
        bt.onClick.RemoveAllListeners();
    }

    UnityEngine.UI.Button tempBt;

    public void OnCraftItemSelected(UnityEngine.UI.Button selectedBT)
    {
        bt.onClick.RemoveAllListeners();
        tempBt = selectedBT;

        
        requieCraftComponents.Remove(null,"Bolts", 1000);
        requieCraftComponents.Remove(null,"Fluid", 1000);
        requieCraftComponents.Remove(null, "Electronics", 1000);
        requieCraftComponentsGrid.GetComponent<UIInventory>().SetupInvntoryUI(requieCraftComponents);

        InventorySlot slot =selectedBT. GetComponentInParent<UICraftSlot>().slot;
        if (slot.isBlock == true)
            return;
        else
        {
            button.SetActive(true);
            requieCraftComponents.TryToAdd(null, new Item(ItemBase.ItemsInfo["Bolts"], (slot.item.info as CraftableItemInfo).requeBolts));
            requieCraftComponents.TryToAdd(null, new Item(ItemBase.ItemsInfo["Fluid"], (slot.item.info as CraftableItemInfo).requeFluid));
            requieCraftComponents.TryToAdd(null, new Item(ItemBase.ItemsInfo["Electronics"], (slot.item.info as CraftableItemInfo).requeElectronics));
            itemSlot.GetComponentInChildren<UIInventorySlot>().SetSlot(slot);
            itemSlot.GetComponentInChildren<UIInventorySlot>().Refresh();
            textName.text = "Название\n"+slot.item.info.title;
            if (slot.item.info.itemType == ItemTypes.RobortParts)
            {
                textHaract.text = "Характеристики\n Коэффициент скорости - " + (slot.item.info as RobotPartInfo).partMoveKf + "\n" +
                                  "Дополнительное здоровье - " + (slot.item.info as RobotPartInfo).partHp + "\n" +
                                  "Дополнительная энергия - " + (slot.item.info as RobotPartInfo).partEn + "\n";
            }
            else if (slot.item.info.itemType == ItemTypes.Perks)
            {
                textHaract.text = "Характеристики\n Дополнительный урон - " + (slot.item.info as PerkInfo).partBaseDamage +" Рузльтирующий урон оружия - "+ (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item == null ? (slot.item.info as PerkInfo).partBaseDamage : (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item.info as WeaponItemInfo).damage+ (slot.item.info as PerkInfo).partBaseDamage)+ "\n" +
                                  "Изменение дальности аттаки - " + (slot.item.info as PerkInfo).partBaseRange + " Рузльтирующая дальность аттаки оружия  - " + (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item == null ? (slot.item.info as PerkInfo).partBaseRange : (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item.info as WeaponItemInfo).range+ (slot.item.info as PerkInfo).partBaseRange) + "\n" +
                                  "Изменение сокрости атаки - " + (slot.item.info as PerkInfo).partBaseCooldown + " Рузльтирующая скорость аттаки оружия - " + (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item == null ? (slot.item.info as PerkInfo).partBaseCooldown : (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item.info as WeaponItemInfo).cooldown + (slot.item.info as PerkInfo).partBaseCooldown) +"\n";
            }
            textDesc.text = "Описание\n"+slot.item.info.description;
            




            if ((playerInventory.craftComponents.GetItemCount("Bolts") >= (slot.item.info as CraftableItemInfo).requeBolts) &&
                (playerInventory.craftComponents.GetItemCount("Fluid") >= (slot.item.info as CraftableItemInfo).requeFluid) &&
                (playerInventory.craftComponents.GetItemCount("Electronics") >= (slot.item.info as CraftableItemInfo).requeElectronics))
            {
                //синюю обводку добавить в кнопку
                bt.interactable = true;
                bt.onClick.AddListener(() => OnButtonClick(tempBt));
            }
            else
            {
                bt.interactable = false;
            }
        } 
    }
}
