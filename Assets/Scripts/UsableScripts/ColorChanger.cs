using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : ExampleUsable
{

    public override void UseMoment()
    {
        subject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);

    }
}
