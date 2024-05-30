using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapHandler : MonoBehaviour
{
    public Image image;
    private void OnEnable()
    {
        image.sprite = MapDictionary.imageDict[SceneManager.GetActiveScene().name];
    }
}
