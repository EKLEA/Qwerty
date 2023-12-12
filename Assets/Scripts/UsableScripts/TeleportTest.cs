using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTest : ExampleUsable
{
    public new GameObject subject { get; private set; }
    public override void UseMoment()
    {

       // subject.gameObject.GetComponent<IMoveHandler>().SetVelocity(new Vector3(0, 200));


    }
    public new void OnTriggerEnter(Collider other)
    {
        subject = other.gameObject;
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent += Cheker;
    }
    public new void OnTriggerExit(Collider other) 
    { 

        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent -= Cheker;
    }
}