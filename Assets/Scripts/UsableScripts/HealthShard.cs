using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthShard : ExampleUsable
{
    public override void UseMoment()
    {
        //коротина поднятия
        PlayerController.Instance.playerLevelList.tempAddHP += 1;

    }
}
