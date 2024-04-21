using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    [SerializeField] GameObject[] maps;

    CheckPoint checkPoint;
    private void OnEnable()
    {
        checkPoint= FindObjectOfType<CheckPoint>();
        if (checkPoint != null)
            if (checkPoint.interacted)
                UpdateMap();
    }
    void UpdateMap()
    {
        var savedScenes = SaveData.Instance.sceneNames;

        for (int i = 0;i < maps.Length;i++)
        {
            if (savedScenes.Contains("TrainingScene" + (i + 1)))
                maps[i].SetActive(true);
            else
                maps[i].SetActive(false);
        }
    }
}
