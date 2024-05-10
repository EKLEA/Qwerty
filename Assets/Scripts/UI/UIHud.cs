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
   
    private GameObject[] heartContainers;
    private Image[] heartFills;
    public Transform heartsParent;
    public GameObject heartsContainerPrefab;
    public Transform energyParent;
    public GameObject energyBarPrefab;
    public GameObject heart;
    public Material enBarMat1;
    public Material enBarMat2;

    public void InitHud()
    {
        PlayerHealthController.Instance.OnHealthChangedCallBack += UpdateHeartHUD;
        PlayerHealthController.Instance.OnEnergyChangedCallBack += UpdateEnergyHUD;
        PlayerHealthController.Instance.OnDeadCallBack += UpdateHeart;
        PlayerHealthController.Instance.OnHealthVarChange += UpdateHud;

        SetupHud();
        

    }
    float tempMaxEn;
    void SetupHud()
    {
        
        heartContainers = new GameObject[(int)PlayerHealthController.Instance.resHealth];
        heartFills = new Image[(int)PlayerHealthController.Instance.resHealth];
        InstantiateHeartContainers();

        UpdateHeartHUD();

        UpdateHeart();

        SetEnergyBar();

        

    }
    private void UpdateHud(object t)
    {
        PlayerHealthController.Instance.OnHealthChangedCallBack -= UpdateHeartHUD;
        PlayerHealthController.Instance.OnEnergyChangedCallBack -= UpdateEnergyHUD;
        PlayerHealthController.Instance.OnDeadCallBack -= UpdateHeart;
        PlayerHealthController.Instance.OnHealthVarChange -= UpdateHud;
        heartContainers = new GameObject[0];
        heartFills = new Image[0];
        SetupHud();

    }

    void SetHeartContainers()
    {

        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < PlayerHealthController.Instance.resHealth)
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
            if (i < PlayerHealthController.Instance.health)
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
        int childCount = energyParent.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(energyParent.transform.GetChild(i).gameObject);
        }

        tempMaxEn =PlayerHealthController.Instance.resEnergy;

        GameObject enBar = Instantiate(energyBarPrefab);
        enBar.transform.SetParent(energyParent, false);

        enBarFill= enBar.transform.Find("EnergyFill").GetComponent <Image>();
        if (PlayerHealthController.Instance.isHeartHas)
            enBarFill.material = enBarMat1;
        else
            enBarFill.material = enBarMat2;



        enBar.transform.localScale = new Vector3(PlayerHealthController.Instance.resEnergy, 0.5f, 0.1f);

        enBarFill.fillAmount = PlayerHealthController.Instance.energy / PlayerHealthController.Instance.resEnergy;

        enBar.transform.localPosition = new Vector3(enBar.transform.localPosition.x + enBar.GetComponent<RectTransform>().rect.width* enBar.transform.localScale.x / 2, enBar.transform.localPosition.y,0.1f);

        UpdateEnergyBar();

    }
    void UpdateEnergyBar()
    {
        enBarFill.fillAmount = PlayerHealthController.Instance.energy / PlayerHealthController.Instance.resEnergy;
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
        if (PlayerHealthController.Instance.isHeartHas)
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