using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static Console;
using static UnityEditor.Progress;

public class Console:MonoBehaviour 
{
   
    bool showConsole = false;
    bool showHelp = false;
    string inputString;
    private GUIStyle customTextFieldStyle;
    List<object> commandList;


    //команды
    DebugCommand< string, int> AddItem;
    DebugCommand< string, int> RemoveItem;
    DebugCommand<int> AddHealth;
    DebugCommand<  int> AddEnergy;
    DebugCommand Help;

    private void Awake()
    {
        

        //команды
        AddItem = new DebugCommand<string, int>("AddItem", "Добавляет предметы в инвентарь", "AddItem <id> <Count>", (  id,count)=> { PlayerInventory.AddItem(id,count); } );
        RemoveItem = new DebugCommand<string, int>("RemoveItem", "Удаляет предметы из инвентаря", "RemoveItem <id> <Count>", ( id,count)=> { PlayerInventory.RemoveItem(id,count); } );
        AddHealth = new DebugCommand<int>("AddHealth", "Добавляет одну ячейку хп", "AddHealth <count>", ( count)=> { PlayerController.Instance.playerHealthController.IncreaseMaxHealth(count); } );
        AddEnergy = new DebugCommand< int>("AddEnergy", "Добавляет одну ячейку tythubb", "AddEnergy <Count>", ( count)=> { PlayerController.Instance.playerHealthController.IncreaseMaxEnergy(count); } );
        Help = new DebugCommand("Help", "Показывает все доступные команды", "Help", () => { showHelp = true; }); 



        commandList = new List<object>
        {
            AddItem,
            RemoveItem,
            AddHealth,
            AddEnergy,
            Help,
        };
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            ToggleConsole();
        }

        if (showConsole && Input.GetKeyDown(KeyCode.Return))
        {
            HandleInput();
            inputString = "";
        }
    }
    public void ToggleConsole()
    {
        showConsole = !showConsole;
    }
    Vector2 scroll;
    private void OnGUI()
    {
        if (!showConsole) return;
        float y = 0f;
        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 240), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 50 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 220), scroll, viewport);
            for(int i = 0; i< commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(10, 60 * i, viewport.width - 100, 60);
                GUI.Label(labelRect, label,customTextFieldStyle);
            }
            GUI.EndScrollView();
            y += 240;
        }
        
        GUI.Box(new Rect(0, y, Screen.width, 60), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        customTextFieldStyle = new GUIStyle(GUI.skin.textField);

        customTextFieldStyle.fontSize =50;
        inputString =GUI.TextField(new Rect(10f,y, Screen.width, 60), inputString,customTextFieldStyle);
    }
    void HandleInput()
    {
        string[] prop = inputString.Split(' ');
        for (int i = 0;i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if(inputString.Contains(commandBase.commandID))
            {
                if (commandList[i] as DebugCommand != null)
                    (commandList[i] as DebugCommand).Invoke();
                else if (commandList[i] as DebugCommand<string, int> != null)
                {
                    if (inputString.Contains("Item"))
                    {
                        int c;

                        if (prop[2] == "max")

                            if (prop[1] == "CraftComponents")
                                c = 1000;
                            else
                                c = ItemBase.ItemsInfo[prop[1]].maxItemsInInventortySlot;
                            else
                            c = Convert.ToInt32(prop[2]);


                        if (prop[1]=="CraftComponents")
                        {
                            (commandList[i] as DebugCommand<string, int>).Invoke("Bolts", c);
                            (commandList[i] as DebugCommand<string, int>).Invoke("Fluid", c);
                            (commandList[i] as DebugCommand<string, int>).Invoke("Electronics", c);
                        }
                            
                        else
                            (commandList[i] as DebugCommand<string, int>).Invoke(prop[1], c);
                    }
                }
                else if (commandList[i] as DebugCommand< int> != null)
                {
                    (commandList[i]as DebugCommand<int>).Invoke(Convert.ToInt32(prop[1]));
                }
                inputString = "";
            }
        }
    }
}
