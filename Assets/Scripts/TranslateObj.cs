using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TranslateOBj : MonoBehaviour,IsUsable
{
    public GameObject Subject => gameObject;
   

    [SerializeField] private PlayerUseMoment _playerUseMoment;
    public PlayerUseMoment playerUseMoment { get; private set; }
    void Start()
    {
        playerUseMoment = _playerUseMoment;
    }

    private void Cheker(object obj)
    {
        UseMoment();
    }
    public void UseMoment ()
    {
        Subject.transform.position = transform.position + new Vector3(0, 2f, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent += Cheker;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent -= Cheker;
    }

}
