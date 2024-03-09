using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject inventoryUIInterface;
    [SerializeField] Camera uiCam;
    public PlayerUseMoment playerUseMoment => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUseMoment>();
    UIInventory uIInventory=> inventoryUIInterface.GetComponent<UIInventory>();
     
   
    private void Start()
    {

        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        inventoryUIInterface.SetActive(false);
        uiCam.gameObject.SetActive(false);
    }
    private void OpenInv(bool t)
    {
        
        if (t)
        {
            inventoryUIInterface.SetActive(true);
            uiCam.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventoryUIInterface.SetActive(false);
            uiCam.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
