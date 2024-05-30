using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDownCast : ExampleUsable
{
    public override void UseMoment()
    {
        PlayerController.Instance.playerLevelList.DownCast = true;
        playerUseMoment.OnUsedEvent -= Cheker;
        DestroyImmediate(gameObject);
    }
}
