using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseMoment : MonoBehaviour
{
    public Action<object> OnUsedEvent;
    [SerializeField] public GameObject Hand;
    public delegate void OnOpenInventoryDelegate();
    [HideInInspector] public OnOpenInventoryDelegate OnOpenInventoryCallBack;
    public delegate void OnOpenConsoleDelegate();
    [HideInInspector] public OnOpenConsoleDelegate OnOpenConsoleCallBack;
    public event Action<bool> OnOpenContextMenuEvent;
    public event Action<float> OnChangeMenuEvent;




    private void Update()
    {
        if (Input.GetButtonDown("Use"))
            OnUsedEvent?.Invoke(this);
        if (Input.GetButtonDown("ChangeMenu"))
            if(!PlayerController.Instance.playerStateList.isDraging)
                OnChangeMenuEvent?.Invoke(Input.GetAxisRaw("ChangeMenu"));
        if (Input.GetButtonDown("Inventory")&& PlayerController.Instance.rb.velocity.x<2f&&PlayerController.Instance.playerStateList.grounded)
            OnOpenInventoryCallBack?.Invoke();
        if (Input.GetMouseButtonDown(1))
            OnOpenContextMenuEvent?.Invoke(true);
        if (Input.GetMouseButtonDown(0))
            if (Input.GetButtonDown("Inventory"))
                OnOpenContextMenuEvent?.Invoke(false);


    }
}

