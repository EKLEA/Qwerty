using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeShard : ExampleUsable
{
   public int _tempHP;
    public override void UseMoment()
    {
        PlayerController.Instance.playerLevelList.tempAddHP += _tempHP;
        StartCoroutine(PlayerController.Instance.TakeShard(true));//енергия;
        playerUseMoment.OnUsedEvent -= Cheker;
        Destroy(gameObject);
        //коротина поднятия
    }
}
