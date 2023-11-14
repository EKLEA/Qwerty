using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerUseMoment : MonoBehaviour
{
    public event Action<object> OnUsedEvent;
    public event Action<object> OnOpenInventoryEvent;
    public event Action<object> OnCloseInventoryEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnUsedEvent?.Invoke(this);
        if (Input.GetKeyDown(KeyCode.I))
            OnOpenInventoryEvent ? .Invoke(this);
        if (Input.GetKeyUp(KeyCode.I))
            OnCloseInventoryEvent ? .Invoke(this);

    }
}
