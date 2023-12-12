using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ExampleUseMoment : MonoBehaviour
{
    public   Action<object> OnUsedEvent;

    public   Action<object> OnSimpleAtackEvent;
    public   Action<object> OnSpecialAtackEvent;

}
