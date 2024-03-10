using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject playerUIInterface;
    [SerializeField] GameObject[] screens;
    [SerializeField] Camera uiCam;
    public PlayerUseMoment playerUseMoment => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUseMoment>();


    int menuID;
    private void Start()
    {

        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerUseMoment.OnChangeMenuEvent += ChangeMenu;
        playerUIInterface.SetActive(false);
        uiCam.gameObject.SetActive(false);
    }
    private void OpenInv(bool t)
    {
        
        
        if (t)
        {
            menuID = 0;
            playerUIInterface.SetActive(true);
            screens[menuID].SetActive(true);
            uiCam.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            playerUIInterface.SetActive(false);
            uiCam.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

    }
    private void ChangeMenu(float s)
    {

        if (!playerUIInterface.activeInHierarchy)
            return;
        else
        {
            
            int t;
            if (s > 0)
                t = 1;
            else if (s < 0)
                t = -1;
            else
                t = 0;
            Debug.Log(t);
            screens[menuID].SetActive(false);
            if (menuID + t >= screens.Length)
                menuID = 0;
            else if (menuID + t < 0)
                menuID = screens.Length - 1;
            else
                menuID += t;
            screens[menuID].SetActive(true);
        }
    }
}
