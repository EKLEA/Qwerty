using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorChanger : MonoBehaviour,IsUsable
{
    [SerializeField] GameObject obj;
    
    public GameObject _operator { get; set; }
    public GameObject _subject { get; set; }

    void Start()
    {
        _subject = obj;
        
    }
   
    public void UseMoment ()
    {
        _subject.GetComponent<Renderer>().material.color = new Color(0, 204, 102);
    }
   
}
