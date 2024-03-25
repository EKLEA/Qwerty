using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class UIStorageController : MonoBehaviour
{


    PlayerInventory playerInventory => PlayerController.Instance.GetComponent<PlayerInventory>();

    public GameObject equippedGrid;
    public GameObject ñraftComponentsItemsGrid;
    public GameObject storageItemsGrid;


    //List<UIInventorySlot> storageSlots=new List<UIInventorySlot>();


    private void Awake()
    {
        equippedGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.equippedItems);
        ñraftComponentsItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.craftComponents);

        storageItemsGrid.GetComponent<UIInventory>().SetupInvntoryUI(playerInventory.storageItems);
    }
    int ids;
    int id
    {
        get { return ids; }
        set 
        {
            if (value < 0)
                ids = 0;
            else if (value > screens.Length)
                ids = screens.Length - 1;
            else
                ids = value;
        }
    }
    private void OnEnable()
    {
        id = 0;
        screens[id].gameObject.SetActive(true);
        upBt.gameObject.SetActive(false);
    }
    GridLayoutGroup[] screens => storageItemsGrid.GetComponentsInChildren<GridLayoutGroup>(includeInactive:true);
    [SerializeField] Button upBt;
    [SerializeField] Button downBt;
    public void SwitchScreen(int t)
    {
        screens[id].gameObject. SetActive(false);

        id += t;

        if (id == screens.Length-1)
        {
            upBt.gameObject.SetActive(true);
            downBt.gameObject.SetActive(false);
        }
        else if (id == 0)
        {
            upBt.gameObject.SetActive(false);
            downBt.gameObject.SetActive(true);
        }
        else
        {
            upBt.gameObject.SetActive(true);
            downBt.gameObject.SetActive(true);
        }
        screens[id].gameObject.SetActive(true);
    }
}
