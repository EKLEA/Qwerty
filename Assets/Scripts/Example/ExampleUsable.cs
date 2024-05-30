using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExampleUsable : MonoBehaviour
{
    public static PlayerUseMoment playerUseMoment;

    void Start()
    {
        playerUseMoment = PlayerController.Instance.GetComponent<PlayerUseMoment>();
    }
    public void Cheker(object s)
    {
        UseMoment();

    }
    /*private void OnDisable()
    {
        playerUseMoment.OnUsedEvent -= Cheker;
    }*/
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
