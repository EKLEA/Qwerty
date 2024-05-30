using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Opisanie : MonoBehaviour
{
    public static Opisanie Instance;
    public GameObject obj;
    public TextMeshProUGUI text;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        gameObject. SetActive(false);
    }
    public void SetText(string _text)
    {
        text.text = _text;
    }
}
