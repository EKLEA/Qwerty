using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerUseMoment : MonoBehaviour
{
    public event Action<object> OnUsedEvent;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            OnUsedEvent?.Invoke(this);
    }
}
