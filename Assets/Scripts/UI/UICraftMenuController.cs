using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CraftMenuScript : MonoBehaviour
{
    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();



    public GameObject �raftComponentsItemsGrid;
    public GameObject craftableItemsGrid;

    private UIInventorySlot[] uiCraftComponentsIntemsSlots => �raftComponentsItemsGrid.GetComponentsInChildren<UIInventorySlot>();
    private UIInventorySlot[] uicraftableItemsSlots => craftableItemsGrid.GetComponentsInChildren<UICraftSlot>();
    [SerializeField] Scrollbar scrollbar;
    private void Start()
    {
        �raftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents, uiCraftComponentsIntemsSlots);
        craftableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftableItems, uicraftableItemsSlots);
        scrollbar= GetComponentInChildren<Scrollbar>();
    }
    private void OnEnable()
    {
        scrollbar.value = 1f;
    }
    public void OnButtonClick()
    {

    }
}
