using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeEnergy : ExampleUsable
{
    public int _tempEN;
    public override void UseMoment()
    {
        PlayerController.Instance.playerLevelList.tempAddEN += _tempEN;
        StartCoroutine(PlayerController.Instance.TakeShard(false));// энергия
        playerUseMoment.OnUsedEvent -= Cheker;
        Destroy(gameObject);

        //коротина поднятия
    }
}
