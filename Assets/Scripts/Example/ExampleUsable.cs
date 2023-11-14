using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExampleUsable : MonoBehaviour
{
    public GameObject subject => gameObject;
    [SerializeField] PlayerUseMoment _playerUseMoment;
    public PlayerUseMoment playerUseMoment =>_playerUseMoment;


    public void Cheker(object obj)
    {
        UseMoment();
    }
    public abstract void UseMoment();
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent += Cheker;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent -= Cheker;
    }
}
