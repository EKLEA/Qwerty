using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTake : ExampleUsable
{
    public override void UseMoment()
    {
        playerUseMoment.gameObject.GetComponent<PlayerInventory>().inventory.TryToAdd(this,subject.GetComponent<Item>().GetExItem());
        gameObject.SetActive(false);
    }
}
