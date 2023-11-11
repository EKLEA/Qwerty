using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranslateOBj : MonoBehaviour,IsUsable
{
    public GameObject Subject => gameObject;
    private bool isTriggered;

   

    [SerializeField] private PlayerUseMoment _playerUseMoment;
    public PlayerUseMoment playerUseMoment { get; private set; }
    void Start()
    {
        playerUseMoment = _playerUseMoment;
        playerUseMoment.OnUsedEvent += Cheker;
    }

    private void Cheker(object obj)
    {
        if (isTriggered)
        {
            UseMoment();
        }
    }
    public void UseMoment ()
    {
        Subject.transform.position = transform.position + new Vector3(0, 2f, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
            isTriggered = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag==("Player"))
            isTriggered = false;
    }

}
