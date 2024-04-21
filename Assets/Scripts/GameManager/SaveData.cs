using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SaveData
{
    public static SaveData Instance;

    //map
    public HashSet<string> sceneNames;
    //checkPoint stuff
    public string checkPointSceneName;
    public Vector3 checkPointPos;
    public void Initialize()
    {
        if (!File.Exists(Application.persistentDataPath + "/save.checkPoint.data"))
        {
            BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.checkPoint.data"));
        }
        if (sceneNames == null)
        {
            sceneNames = new HashSet<string>();
        }
    }
    public void SaveCheckPoint()
    {
        using(BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.checkPoint.data")))
        {
            writer.Write(checkPointSceneName);
            writer.Write(checkPointPos.x);
            writer.Write(checkPointPos.y);
            writer.Write(checkPointPos.z);
        }
    }

    public void LoadCheckPoint()
    {
        if(File.Exists(Application.persistentDataPath + "/save.checkPoint.data"))
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead((Application.persistentDataPath + "/save.checkPoint.data"))))
            {
                checkPointSceneName=reader.ReadString();
                checkPointPos.x=reader.ReadSingle();
                checkPointPos.y=reader.ReadSingle();
                checkPointPos.z=reader.ReadSingle();
            }
        }
    }
}
