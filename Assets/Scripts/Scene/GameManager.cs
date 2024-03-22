using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;
    public Vector2 platformingRespawnPoint;
    public Vector2 respawnPoint;
    [SerializeField] CheckPoint checkPoint;
    public static GameManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        checkPoint=FindObjectOfType<CheckPoint>();
    }
    public void RespawnPlayer()
    {
        if(checkPoint!=null)
        {
            if (checkPoint.interacted)
                respawnPoint = checkPoint.transform.position;
            else
                respawnPoint = platformingRespawnPoint;
        }
        else
            respawnPoint = platformingRespawnPoint;
        PlayerController.Instance.transform.position = respawnPoint;
        StartCoroutine(UIController.Instance.DeactivateDeathScreen());
        PlayerController.Instance.Respawned();
    }
}
