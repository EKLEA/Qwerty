using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIHud : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject[] heartContainers;
    private Image[] heartFills;
    public Transform heartsParent;
    public GameObject heartsContainerPrefab;
    public Transform energyParent;
    public GameObject energyBarPrefab;

    private void Start()
    {

        playerController = PlayerController.Instance;
        StartCoroutine(SetupHud());
        playerController.playerHealthController.OnHealthChangedCallBack += UpdateHeartHUD;
        playerController.playerHealthController.OnEnergyChangedCallBack += UpdateEnergyHUD;


    }
    float tempMaxEn;
    IEnumerator SetupHud()
    {
        yield return new WaitForSeconds(0.001f);
        heartContainers = new GameObject[(int)playerController.playerHealthController.resHealth];
        heartFills = new Image[(int)playerController.playerHealthController.resHealth];
        InstantiateHeartContainers();
        SetEnergyBar();
        UpdateHeartHUD();

        
    }
    private void UpdateHud()
    {
        for (int i = heartsParent.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(heartsParent.transform.GetChild(i).gameObject);
        }
        heartContainers = new GameObject[0];
        heartFills = new Image[0];
        StartCoroutine(SetupHud());

    }

    void SetHeartContainers()
    {

        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < playerController.playerHealthController.resHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }
    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < playerController.playerHealthController.health)
            {
                heartFills[i].color = Color.green;
            }
            else
            {
                heartFills[i].color = Color.black;
            }
        }
    }
    void InstantiateHeartContainers()
    {
        
        for (int i = 0; i < heartContainers.Length; i++)
        {
            GameObject temp = Instantiate(heartsContainerPrefab);
            temp.transform.SetParent(heartsParent,false);
            heartContainers[i]=temp;
            heartFills[i]=temp.transform.Find("HpFill").GetComponent<Image>();
        }
    }
    private GameObject enBar;
    private Image enBarFill;
    void SetEnergyBar()
    {
        if (energyParent.transform.childCount!=0)
            DestroyImmediate(energyParent.transform.GetChild(0).gameObject);

        tempMaxEn =playerController.playerHealthController.resEnergy;

        GameObject enBar = Instantiate(energyBarPrefab);
        enBar.transform.SetParent(energyParent, false);

        enBarFill= enBar.transform.Find("EnergyFill").GetComponent <Image>();


        enBar.transform.localScale = new Vector3(playerController.playerHealthController.resEnergy, 0.5f,0.1f);

        enBarFill.fillAmount = playerController.playerHealthController.energy / playerController.playerHealthController.resEnergy;

        enBar.transform.localPosition = new Vector3(enBar.transform.localPosition.x + enBar.GetComponent<RectTransform>().rect.width* enBar.transform.localScale.x / 2, enBar.transform.localPosition.y,0.1f);

        UpdateEnergyBar();

    }
    void UpdateEnergyBar()
    {
        enBarFill.fillAmount = playerController.playerHealthController.energy / playerController.playerHealthController.resEnergy;
    }
    void UpdateHeartHUD()
    {
        if (heartContainers.Length != (int)playerController.playerHealthController.resHealth)
            UpdateHud();

        SetHeartContainers();
        SetFilledHearts();
    }
    void UpdateEnergyHUD()
    {
        if (tempMaxEn != playerController.playerHealthController.resEnergy)
            UpdateHud();
        UpdateEnergyBar();
    }

}