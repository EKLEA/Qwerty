using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;
    public Vector3 platformingRespawnPoint;
    public Vector3 respawnPoint;
    public GameObject heartEnemy;
    [SerializeField] CheckPoint checkPoint;


    [SerializeField] private FadeUI pauseMenu;

    [SerializeField] float fadeTime;

    public bool gameIsPaused;

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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveData.Instance.SavePlayerData();
            Debug.Log(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
        {
            pauseMenu.StopAllCoroutines();
            pauseMenu.FadeUIIn(fadeTime);
            Time.timeScale = 0;
            gameIsPaused = true;
        }
    }
    public void UnpauseGame()
    {

        Time.timeScale = 1;
        gameIsPaused = false;
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
        {
            respawnPoint = platformingRespawnPoint;
        }

        FollowPlayer.Instance.transform.position = new Vector3(respawnPoint.x, respawnPoint.y, respawnPoint.z - 20.75f);
        PlayerController.Instance.transform.position = respawnPoint;
        
        StartCoroutine(UIController.Instance.DeactivateDeathScreen());
        PlayerController.Instance.Respawned();

        SaveData.Instance.SaveCheckPoint();
        SaveData.Instance.SavePlayerData();
        SaveData.Instance.SavePlayerLevelListData();
        SaveData.Instance.SavePlayerInv();
    }
}
