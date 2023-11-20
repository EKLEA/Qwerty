using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateInventory : ExampleUsable
{
    public new GameObject subject { get; private set; }
    public override void UseMoment()
    {
        subject.GetComponent<PlayerInventory>().invCapacity += 5; ;
    }
    public new void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            subject = other.gameObject;
            playerUseMoment.OnUsedEvent += Cheker;
        }
    }
}
