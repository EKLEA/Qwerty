using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemTakeDrop : ExampleUsable
{
    
    public override void UseMoment()
    {

        gameObject.transform.parent = playerUseMoment.Hand.transform;
        transform.localPosition = new Vector3(-0.67f, -0.036f, -0.013f);
        transform.localRotation = Quaternion.Euler(0, -90, 90);
        playerUseMoment.OnUsedEvent -= Cheker;
    }
}
