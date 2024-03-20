using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject playerUIInterface;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject[] screens;
    [SerializeField] Camera uiCam;
    public PlayerUseMoment playerUseMoment;


    int menuID;
    private void Start()
    {
        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerUseMoment.OnChangeMenuEvent += ChangeMenu;
        playerUseMoment.GetComponent<PlayerController>().playerHealthController.OnDeadCallBack += StartDeathCoroutine;
        playerUIInterface.SetActive(false);
    }
    void StartDeathCoroutine()
    {
        StartCoroutine(ActivateDeathScreen());
    }
    private void OpenInv(bool t)
    {
        
        
        if (t)
        {
            menuID = 0;
            playerUIInterface.SetActive(true);
            screens[menuID].SetActive(true);
            Time.timeScale = 0.5f;
        }
        else
        {
            playerUIInterface.SetActive(false);
            screens[menuID].SetActive(false);
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
    public IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.8f);
        //scene fader
        yield return new WaitForSeconds(0.8f);
        deathScreen.SetActive(true);
    }
}
