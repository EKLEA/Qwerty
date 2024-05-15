using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DelBT : MonoBehaviour, IPointerEnterHandler
{
    public string SavePath;
    Button bt =>GetComponent<Button>();
    UIAudio uiAudio => GetComponentInParent<UIAudio>();
    private void OnEnable()
    {
        if (Directory.Exists(Application.persistentDataPath + "/" +SavePath) && File.Exists(Application.persistentDataPath + "/"+ SavePath + "/save.checkPoint.data"))
        {
            bt.interactable = true;
        }
        else
        {
            bt.interactable = false;
        }
    }
    public void DeletePath()
    {
        if (Directory.Exists(Application.persistentDataPath + "/" + SavePath))
        {
            Directory.Delete(Application.persistentDataPath + "/"+SavePath,true);
            bt.interactable = false;
        }
        else
        {
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!bt.interactable)
            return;
        else
            uiAudio.SoundOnHover();
    }
}
