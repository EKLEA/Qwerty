using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSideCast : ExampleUsable
{
    public override void UseMoment()
    {
        PlayerController.Instance.playerLevelList.SideCast = true;
        playerUseMoment.OnUsedEvent -= Cheker;
        DestroyImmediate(gameObject);
    }
}
