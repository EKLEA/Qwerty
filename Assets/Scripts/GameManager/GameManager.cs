using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;
    public Vector3 platformingRespawnPoint;
    public Vector3 respawnPoint;
    [SerializeField] CheckPoint checkPoint;
    public static GameManager Instance { get; private set; }
    public void Awake()
    {
        SaveData.Instance.Initialize();
        if (Instance != null && Instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        SaveScene();
        DontDestroyOnLoad(gameObject);
        checkPoint=FindObjectOfType<CheckPoint>();
    }

    public void SaveScene()
    {
        string currectSceneName = SceneManager.GetActiveScene().name;
        SaveData.Instance.sceneNames.Add(currectSceneName);
    }
    public void RespawnPlayer()
    {
        transitionedFromScene = "";
        PlayerController.Instance.playerStateList.respawning = true;
        SaveData.Instance.LoadCheckPoint();
        if(SaveData.Instance.checkPointSceneName!=null)
        {
            SceneManager.LoadScene(SaveData.Instance.checkPointSceneName);
        }
        if (SaveData.Instance.checkPointPos != null)
            respawnPoint = SaveData.Instance.checkPointPos;
        else
            respawnPoint = platformingRespawnPoint;

        
        PlayerController.Instance.transform.position = respawnPoint;
        FollowPlayer.Instance.transform.position = new Vector3(respawnPoint.x, respawnPoint.y, respawnPoint.z - 20.75f);
        StartCoroutine(UIController.Instance.DeactivateDeathScreen());
        PlayerController.Instance.Respawned();
    }
}
