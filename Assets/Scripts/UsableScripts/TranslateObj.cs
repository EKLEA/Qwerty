using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranslateOBj : ExampleUsable
{
    public override void UseMoment()
    {
        subject.transform.position = transform.position + new Vector3(0, 2f, 0);
    }
}
