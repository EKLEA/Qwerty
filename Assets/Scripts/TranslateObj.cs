using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranslateOBj : MonoBehaviour,IsUsable
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
        _subject.transform.position = transform.position + new Vector3(0, 2f, 0);
    }
   
}
