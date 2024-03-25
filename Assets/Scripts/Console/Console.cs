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
    DebugCommand<InventoryWithSlots, string, int> CheatAdd;
    DebugCommand<InventoryWithSlots, string, int> CheatRemove;
    DebugCommand Help;

    private void Awake()
    {
        

        //команды
        CheatAdd = new DebugCommand<InventoryWithSlots, string, int>("CheatAdd", "Добавляет предметы в инвентарь","CheatAdd <Inventory> <id> <Count>",( inv, id,count)=> { PlayerInventory.CheatAdd(inv,id,count); } );
        CheatRemove = new DebugCommand<InventoryWithSlots, string, int>("CheatRemove", "Удаляет предметы из инвентаря", "CheatRemove <Inventory> <id> <Count>", ( inv, id,count)=> { PlayerInventory.CheatRemove(inv,id,count); } );
        Help = new DebugCommand("Help", "Показывает все доступные команды", "Help", () => { showHelp = true; }); 



        commandList = new List<object>
        {
            CheatAdd,
            CheatRemove,
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
                else if (commandList[i] as DebugCommand<InventoryWithSlots,string,int> != null)
                {

                    InventoryWithSlots inv = (InventoryWithSlots)typeof(PlayerInventory).GetField(prop[1]).GetValue(PlayerInventory.Instance);
                        (commandList[i] as DebugCommand<InventoryWithSlots, string, int>).Invoke(inv, prop[2], Convert.ToInt32(prop[3]));


                }
                inputString = "";
            }
        }
    }
}
