using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHeart : ExampleUsable
{
    public override void UseMoment()
    {
        playerUseMoment.OnUsedEvent -= Cheker;
        PlayerController.Instance.playerHealthController.isHeartHas = true;
        UIController.Instance.uiHud.GetComponent<UIHud>().InitHud();
        SaveData.Instance.SavePlayerData();
        
        Destroy(gameObject);
    }
}
