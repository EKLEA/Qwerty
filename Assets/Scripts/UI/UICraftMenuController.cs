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

    [SerializeField] Scrollbar scrollbar;
    private void Awake()
    {
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);
        craftableItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftableItems);
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
