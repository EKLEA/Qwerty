using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CraftMenuScript : MonoBehaviour
{

    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();


    public GameObject ñraftComponentsItemsGrid;
    public GameObject craftableItemsGrid;
    public GameObject scrollbar;
    Scrollbar _scrollbar=>scrollbar.GetComponent<Scrollbar>();
    private void Awake()
    {
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);
        craftableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftableItems);
    }
    private void OnEnable()
    {
        craftableItemsGrid.transform.localPosition = new Vector3(0, -1060f, 0f);
    }
    public void OnButtonClick()
    {

    }
}
