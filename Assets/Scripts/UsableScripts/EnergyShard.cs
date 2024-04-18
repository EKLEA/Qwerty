using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShard : ExampleUsable
{
    public override void UseMoment()
    {
        //коротина поднятия
        PlayerController.Instance.playerLevelList.tempAddEN += 1;

    }
}
