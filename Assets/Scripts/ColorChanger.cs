using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : MonoBehaviour,IsUsable
{
    [SerializeField] GameObject obj;

    public GameObject Subject => obj;

    public void UseMoment ()
    {
        Subject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
    }
   
}
