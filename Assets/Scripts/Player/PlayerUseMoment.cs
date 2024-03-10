using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerUseMoment : ExampleUseMoment
{
    [SerializeField] public GameObject Hand;
    public event Action<bool> OnOpenInventoryEvent;
    public event Action<bool> OnOpenContextMenuEvent;
    public event Action<float> OnChangeMenuEvent;



    private void Update()
    {
        if (Input.GetButtonDown("Use"))
            OnUsedEvent?.Invoke(this);
        if (Input.GetButtonDown("ChangeMenu"))
            OnChangeMenuEvent?.Invoke(Input.GetAxisRaw("ChangeMenu"));
        if (Input.GetButtonDown("Inventory"))
            OnOpenInventoryEvent?.Invoke(true);
        if (Input.GetButtonUp("Inventory"))
            OnOpenInventoryEvent?.Invoke(false);
        if (Input.GetMouseButtonDown(1))
            OnOpenContextMenuEvent?.Invoke(true);
        if (Input.GetMouseButtonDown(0))
            if (Input.GetButtonDown("Inventory"))
                OnOpenContextMenuEvent?.Invoke(false);

    }
}

