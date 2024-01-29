using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject inventoryUIInterface;
    public Camera uiCam;

     UIInventory uIInventory=> inventoryUIInterface.GetComponent<UIInventory>();
    private void Start()
    {
        uIInventory.playerUseMoment.OnOpenInventoryEvent += OpenInv;
        inventoryUIInterface.SetActive(false);
        uiCam.gameObject.SetActive(false);
    }
    private void OpenInv(bool t)
    {
        // тут остановку времени ебнуть
        if (t)
        {
            inventoryUIInterface.SetActive(true);
            uiCam.gameObject.SetActive(true);
        }
        else
            if (uIInventory. chek())
            {
                inventoryUIInterface.SetActive(false);
                uiCam.gameObject.SetActive(false);
            }

    }
}
