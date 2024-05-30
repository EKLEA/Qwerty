using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public struct MapDictionary 
{
    public static MapDictionary Instance;
    public static Dictionary<string, Sprite> imageDict = new Dictionary<string, Sprite>();
    public void Initialize()
    {
        Object[] images= Resources.LoadAll<Sprite>("");
        foreach (object obj in images)
        {

            imageDict.Add((obj as Sprite).name, (obj as Sprite));
        }
    }

   
}
