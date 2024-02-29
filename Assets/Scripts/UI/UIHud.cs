using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIHud : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private GameObject[] heartContainers;
    private Image[] heartFills;
    public Transform heartsParent;
    public GameObject heartsContainerPrefab;
    public Transform energyParent;
    public GameObject energyBarPrefab;
    

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        heartContainers = new GameObject[playerController.playerHealthController.maxHealth];
        heartFills = new Image[playerController.playerHealthController.maxHealth];

        playerController.playerHealthController.OnHealthChangedCallBack += UpdateHeartHUD;
        playerController.playerHealthController.OnEnergyChangedCallBack += UpdateEnergyHUD;

        InstantiateHeartContainers();
        SetEnergyBar();
        UpdateHeartHUD();
    }

    private void Update()
    {
        
    }
    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < playerController.playerHealthController.maxHealth)
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
        GameObject enBar = Instantiate(energyBarPrefab);
        enBar.transform.SetParent(energyParent, false);
        enBarFill= enBar.transform.Find("EnergyFill").GetComponent <Image>();
        enBar.transform.localScale = new Vector3(playerController.playerHealthController.maxEnergy, 0.5f,0.1f);
        enBarFill.fillAmount = playerController.playerHealthController.energy / playerController.playerHealthController.maxEnergy;
        enBar.transform.localPosition = new Vector3(enBar.transform.localPosition.x + enBar.GetComponent<RectTransform>().rect.width* enBar.transform.localScale.x / 2, enBar.transform.localPosition.y,0.1f);
        


    }
    void UpdateEnergyBar()
    {
        enBarFill.fillAmount = playerController.playerHealthController.energy / playerController.playerHealthController.maxEnergy;
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

}