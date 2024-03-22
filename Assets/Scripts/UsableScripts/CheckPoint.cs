using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : ExampleUsable
{
    public bool interacted;
    public override void UseMoment()
    {
        interacted = true;
    }
}
