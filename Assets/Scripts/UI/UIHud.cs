using TMPro;
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

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        heartContainers = new GameObject[playerController.playerHealthController.maxHealth];
        heartFills = new Image[playerController.playerHealthController.maxHealth];

        playerController.playerHealthController.OnHealthChangedCallBack += UpdateHeartHUD;

        InstantiateHeartContainers();
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
    void UpdateHeartHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }
}