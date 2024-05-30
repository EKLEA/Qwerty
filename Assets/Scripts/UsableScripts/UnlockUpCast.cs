using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockUpCast : ExampleUsable
{
    public override void UseMoment()
    {
        PlayerController.Instance.playerLevelList.UPCast = true;
        playerUseMoment.OnUsedEvent -= Cheker;
        DestroyImmediate(gameObject);
    }
}
