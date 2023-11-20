using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class ContextMenu : MonoBehaviour
{
   
    public GameObject contextMenu;
    public Button option1Button;
    public Button option2Button;
    
    private void Start()
    {
        option1Button.onClick.AddListener(Option1Callback);
        option2Button.onClick.AddListener(Option2Callback);
        
    }

    private void Option1Callback()
    {
        Debug.Log("Option 1 selected!");
        contextMenu.SetActive(false);
    }

    private void Option2Callback()
    {
        Debug.Log("Option 2 selected!");
        contextMenu.SetActive(false);
    }
}
