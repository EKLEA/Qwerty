using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetUpdateShards : MonoBehaviour
{
    public Transform healthUPD;
    public GameObject healthBarPrefab;
    public Transform energyUPD;
    public GameObject energyBarPrefab;
    public GameObject heart;


    Image enBarFill;
    Image hpBarFill;

    private void Start()
    {
        SetBars();
        PlayerController.Instance.playerLevelList.OnShardAdded += UpdateBars;
    }
    private void OnEnable()
    {
        if(PlayerController.Instance.playerHealthController.isHeartHas)
            heart.GetComponent<Image>().color = new Color(121 / 256f, 211 / 256f, 255 / 256f);
        else
            heart.GetComponent<Image>().color = new Color(248 / 256f, 23 / 256f, 62 / 256f);
    }
    void SetBars()
    {

        float cH = PlayerController.Instance.playerLevelList.tempAddHP;
        float cE = PlayerController.Instance.playerLevelList.tempAddEN;

        GameObject enBar = Instantiate(energyBarPrefab);
        GameObject hpBar = Instantiate(healthBarPrefab);

        hpBar.transform.SetParent(healthUPD, false);
        enBar.transform.SetParent(energyUPD, false);

        enBarFill = enBar.transform.Find("ENFill").GetComponent<Image>();
        hpBarFill =hpBar.transform.Find("HPFill").GetComponent<Image>();

        healthUPD.transform.localScale = new Vector3(1, 4, 1);
        energyUPD.transform.localScale = new Vector3(1,4, 1);

        UpdateBars(null);

    }
    void UpdateBars(object t)
    {
        enBarFill.fillAmount = PlayerController.Instance.playerLevelList.tempAddEN / 4;
        hpBarFill.fillAmount = PlayerController.Instance.playerLevelList.tempAddHP / 4;
    }
}
