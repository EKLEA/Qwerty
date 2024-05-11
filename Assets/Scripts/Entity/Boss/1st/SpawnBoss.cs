using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
   public static SpawnBoss Instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject boss;
    [SerializeField] Vector2 exitDirection;
    bool callOnce;
    BoxCollider boxCollider;
    private void Awake()
    {
        if(Instance!=null && Instance!=this)
            Destroy(gameObject); 
        else
            Instance = this;
    }
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!callOnce)
            {
                StartCoroutine(WalkIntoRoom());
                callOnce = true;
            }
        }
    }
    IEnumerator WalkIntoRoom()
    {
        StartCoroutine(PlayerController.Instance.moveHandler.WalkIntoNewScene(exitDirection, 1));
        yield return new WaitForSeconds(1f);
        boxCollider.isTrigger = false;
        Instantiate(boss,spawnPoint.position,Quaternion.identity);
    }
    public void IsNotTrigger()
    {
        boxCollider.isTrigger = true;
    }
}
