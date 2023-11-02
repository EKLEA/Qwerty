using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranslateOBj : MonoBehaviour,IsUsable
{
    [SerializeField] GameObject obj;
    
  
    public GameObject Subject => obj;
    public void UseMoment ()
    {
        Subject.transform.position = transform.position + new Vector3(0, 2f, 0);
    }
   
}
