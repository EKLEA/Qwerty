using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject playerUIInterface;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject[] screens;
    [SerializeField] GameObject uiHud;
    public static UIController Instance;
    Camera uiCam;
    PlayerUseMoment playerUseMoment=> PlayerController.Instance.GetComponent<PlayerUseMoment>();


    int menuID;
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
    private void Start()
    {
        
        uiCam= FollowPlayer.Instance.GetComponent<Camera>();
        playerUseMoment.OnOpenInventoryCallBack += OpenInv;
        playerUseMoment.OnChangeMenuEvent += ChangeMenu;
        playerUIInterface.SetActive(false);
    }
    private void OpenInv()
    {
        if (playerUIInterface.activeInHierarchy == false)
            StartCoroutine(OpenInventory());
        else
            StartCoroutine(CloseInventory());

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

            screens[menuID].SetActive(false);

            menuID += t;

            if (menuID == screens.Length)
                menuID = 0;
            else if (menuID < 0)
                menuID = screens.Length - 1;

            if (screens[menuID].CompareTag("CheckPointMenu") && !PlayerController.Instance.playerStateList.interactedWithCheckPoint)
                menuID += t;

            if (menuID >= screens.Length)
                menuID = 0;
            else if (menuID < 0)
                menuID = screens.Length - 1;

            screens[menuID].SetActive(true);
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
    }
    public IEnumerator OpenInventory()
    {
        
        PlayerController.Instance.playerStateList.invOpened = true;
        // анимация открытия
        yield return new WaitForSeconds(0.07f);
        
        menuID = 0;
        uiHud.SetActive(false);
        playerUIInterface.SetActive(true);
        screens[menuID].SetActive(true);
    }
    public IEnumerator CloseInventory()
    {
        
        // анимация закрытия
        yield return new WaitForSeconds(0.07f);
        playerUIInterface.SetActive(false);
        screens[menuID].SetActive(false);
        uiHud.SetActive(true);
        PlayerController.Instance.playerStateList.invOpened = false;
    }
}
