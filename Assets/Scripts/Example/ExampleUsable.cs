using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class ExampleUsable : MonoBehaviour
{
    public GameObject subject;
    public static PlayerUseMoment playerUseMoment;

    void OnEnable()
    {
        subject = gameObject;
        playerUseMoment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUseMoment>();
    }
    public void Cheker(object s)
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
