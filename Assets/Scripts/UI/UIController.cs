using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject playerUIInterface;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject[] screens;
    [SerializeField] public GameObject uiHud;
    [SerializeField] GameObject Console;
    [SerializeField] public GameObject mapHandler;


    public static UIController Instance;
    Camera uiCam;
    PlayerUseMoment playerUseMoment;


    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        sceneFader = GetComponentInChildren<SceneFader>();
       
    }

    public SceneFader sceneFader;
    public void InitUIController()
    {
        playerUseMoment = PlayerController.Instance.gameObject.GetComponent<PlayerUseMoment>();
        uiCam = FollowPlayer.Instance.GetComponent<Camera>();
        playerUseMoment.OnOpenInventoryCallBack += OpenInv;
        playerUseMoment.OnChangeMenuEvent += ChangeMenu;
        playerUIInterface.SetActive(false);
        foreach (GameObject screen in screens)
            screen.GetComponent<UIInventoryScreen>().InitUISceen();
        PlayerInventory.Instance.blockInv = true;
        PlayerInventory.Instance.BlockPlayerInv();

    }
    private void OpenInv()
    {
        if (playerUIInterface.activeInHierarchy == false)
            StartCoroutine(OpenInventory());
        else
        {
            if (screens[menuID].GetComponentInChildren<EquippedMenuController>() != null)
            {
                if (screens[menuID].GetComponentInChildren<EquippedMenuController>().CheckSlots())
                    StartCoroutine(CloseInventory());

            }
            else StartCoroutine(CloseInventory());
        }

    }
    int id;
    int menuID
    {
        get
        {

            
            return id;
        }
        set
        {
            
            if (value == screens.Length)
                id = 0;

            else if (value < 0)
                id = screens.Length - 1;
            else
                id = value;

            


        }
    }
    private void ChangeMenu(float s)
    {

        if (!playerUIInterface.activeInHierarchy)
            return;
        else
        {
            if (screens[menuID].GetComponentInChildren<EquippedMenuController>() != null)
            {
                if (screens[menuID].GetComponentInChildren<EquippedMenuController>().CheckSlots())
                {
                    screens[menuID].SetActive(false);
                   
                    int t;
                    if (s > 0)
                        t = 1;
                    else if (s < 0)
                        t = -1;
                    else
                        t = 0;

                    menuID += t;
                    while (screens[menuID].CompareTag("CheckPointMenu") && !PlayerController.Instance.playerStateList.interactedWithCheckPoint)
                        menuID += 1;
                    screens[menuID].SetActive(true);
                    screens[menuID].GetComponent<UIInventoryScreen>().InitUISceen();





                }
            }
            else
            {
                screens[menuID].SetActive(false);

                int t;
                if (s > 0)
                    t = 1;
                else if (s < 0)
                    t = -1;
                else
                    t = 0;

                menuID += t;
                while (screens[menuID].CompareTag("CheckPointMenu") && !PlayerController.Instance.playerStateList.interactedWithCheckPoint)
                    menuID += 1;
                screens[menuID].SetActive(true);
                screens[menuID].GetComponent<UIInventoryScreen>().InitUISceen();
            }
        }
            
    }
    public IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.In));
        yield return new WaitForSeconds(0.8f);
        deathScreen.SetActive(true);
    }
    public IEnumerator DeactivateDeathScreen()
    {
        
        yield return new WaitForSeconds(0.5f);
        deathScreen.SetActive(false);
        StartCoroutine(sceneFader.Fade(SceneFader.FadeDirection.Out));
        uiHud.GetComponent<UIHud>().InitHud();
    }
    public IEnumerator OpenInventory()
    {
        
        PlayerController.Instance.playerStateList.invOpened = true;
        // �������� ��������
        yield return new WaitForSeconds(0.07f);
        
        menuID = 0;
        screens[menuID].GetComponent<UIInventoryScreen>().InitUISceen();
        uiHud.SetActive(false);
        playerUIInterface.SetActive(true);
        screens[menuID].SetActive(true);
    }
    public IEnumerator CloseInventory()
    {
        
        // �������� ��������
        yield return new WaitForSeconds(0.07f);
        playerUIInterface.SetActive(false);
        screens[menuID].GetComponent<UIInventoryScreen>().InitUISceen();
        screens[menuID].SetActive(false);
        uiHud.SetActive(true);
        PlayerController.Instance.playerStateList.invOpened = false;
    }
}
