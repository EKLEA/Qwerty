using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : MonoBehaviour,IsUsable
{ 

    public GameObject Subject => gameObject;
    private bool isTriggered;

    [SerializeField] private PlayerUseMoment _playerUseMoment;
    public PlayerUseMoment playerUseMoment { get; private set; }
    void Start()
    {
        playerUseMoment = _playerUseMoment;
        playerUseMoment.OnUsedEvent += Cheker;
    }

    private void Cheker(object obj)
    {
        if (isTriggered)
        {
            UseMoment();
        }
    }

    public void UseMoment ()
    {
        Subject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
    }
   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isTriggered = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            isTriggered = false;
    }

}
