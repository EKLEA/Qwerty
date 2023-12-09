using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerUseMoment : MonoBehaviour
{
    public event Action<object> OnUsedEvent;
    public event Action<bool> OnOpenInventoryEvent;
    public event Action<bool> OnOpenContextMenuEvent;

    public GameObject Hand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnUsedEvent?.Invoke(this);
        if (Input.GetKeyDown(KeyCode.I))
            OnOpenInventoryEvent ? .Invoke(true);
        if (Input.GetKeyUp(KeyCode.I))
            OnOpenInventoryEvent? .Invoke(false);
        if (Input.GetMouseButtonDown(1))
            OnOpenContextMenuEvent?.Invoke(true);
        if (Input.GetMouseButtonDown(0))
            OnOpenContextMenuEvent?.Invoke(false);

    }
}
