using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SaveBT : MonoBehaviour
{
    public string SavePath;
    Button bt => GetComponent<Button>();
    private void OnEnable()
    {

        string numbersOnly = new string(SavePath.Where(char.IsDigit).ToArray());
        if (Directory.Exists(Application.persistentDataPath + "/" + SavePath) && File.Exists(Application.persistentDataPath + "/" + SavePath + "/save.checkPoint.data"))
        {
            bt.GetComponentInChildren<TextMeshProUGUI>().text = "Продолжить "+numbersOnly;
        }
        else
        {
            bt.GetComponentInChildren<TextMeshProUGUI>().text = "Сохранение "+numbersOnly;
        }
    }
    public void SetSavePath()
    {
        SaveData.SavePath = "/"+SavePath;
    }
}
