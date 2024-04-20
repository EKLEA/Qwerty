using System.Collections;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
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
    public GameObject heart;
    public Material enBarMat1;
    public Material enBarMat2;

    private void Start()
    {

        playerController = PlayerController.Instance;
        StartCoroutine(SetupHud());
        playerController.playerHealthController.OnHealthChangedCallBack += UpdateHeartHUD;
        playerController.playerHealthController.OnEnergyChangedCallBack += UpdateEnergyHUD;
        playerController.playerHealthController.OnDeadCallBack+=UpdateHeart;
        playerController.playerHealthController.OnHealthVarChange += UpdateHud;

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
        UpdateHeart();


    }
    private void UpdateHud(object t)
    {
        
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
        int childCount = heartsParent.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(heartsParent.transform.GetChild(i).gameObject);
        }

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
        if (playerController.playerHealthController.isHeartHas)
            enBarFill.material = enBarMat1;
        else
            enBarFill.material = enBarMat2;



        enBar.transform.localScale = new Vector3(playerController.playerHealthController.resEnergy, 0.5f, 0.1f);

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
        SetHeartContainers();
        SetFilledHearts();
    }
    void UpdateEnergyHUD()
    {
        UpdateEnergyBar();
    }
    public void UpdateHeart()//переписать тут
    {
        if (playerController.playerHealthController.isHeartHas)
        {
            heart.GetComponent<Image>().color = new Color(121 / 256f, 211 / 256f, 255 / 256f);
        }
        else
        {
            heart.GetComponent<Image>().color = new Color(248 / 256f, 23 / 256f, 62 / 256f);
        }
        SetEnergyBar();

    }
}