
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static Console;


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
    DebugCommand< string, int> RemovePlayerVar;
    DebugCommand<int> AddHealth;
    DebugCommand<int> SetActHeart;
    DebugCommand<int> SetInvLevel;
    DebugCommand<  int> AddEnergy;
    DebugCommand<  int,int> AddShard;
    DebugCommand Kill;

    DebugCommand ItemsID;
    DebugCommand Help;

    private void Awake()
    {
        

        //команды
        AddItem = new DebugCommand<string, int>("AddItem", "Добавляет предметы в инвентарь", "AddItem <id> <Count>", (  id,count)=> { PlayerInventory.AddItem(id,count); } );
        RemoveItem = new DebugCommand<string, int>("RemoveItem", "Удаляет предметы из инвентаря", "RemoveItem <id> <Count>", ( id,count)=> { PlayerInventory.RemoveItem(id,count); } );
        AddHealth = new DebugCommand<int>("AddHealth", "Добавляет одну ячейку хп", "AddHealth <count>", ( count)=> { PlayerController.Instance.playerHealthController.IncreaseMaxHealth(count); } );
        AddEnergy = new DebugCommand< int>("AddEnergy", "Добавляет одну ячейку tythubb", "AddEnergy <Count>", ( count)=> { PlayerController.Instance.playerHealthController.IncreaseMaxEnergy(count); } );
        AddShard = new DebugCommand<int, int>("AddShard", "Добавляет ячейки хп и энкргии", "AddShard <Count of Hp Shard> <Count of En Shard> ", (cHp, cEn) =>
        {

            PlayerController.Instance.playerLevelList.tempAddHP += cHp;
            PlayerController.Instance.playerLevelList.tempAddEN += cEn;
        });
        Kill = new DebugCommand("Kill", "Убивает игрока", "Kill", () => { PlayerController.Instance.playerHealthController.DamageMoment(1000f, Vector2.zero, 0f); });
        SetActHeart= new DebugCommand<int>("SetActHeart", "Меняет значение сердца игрока", "SetActHeart <1 or 0>", (t) => 
        {
            if (t == 1)
                PlayerController.Instance.playerHealthController.isHeartHas = true;
            else if (t == 0)
                PlayerController.Instance.playerHealthController.isHeartHas = false;

        });
        SetInvLevel = new DebugCommand<int>("SetInvLevel", "Меняет значение уровня инвентаря", "SetInvLevel <0-2>", (t) =>
        {
           PlayerController.Instance.playerLevelList.levelTier = t;

        });
        RemovePlayerVar = new DebugCommand<string,int>("RemovePlayerVar", "Меняет значение хп или энергии игрока", "RemovePlayerVar <health or energy> <var>", (c,t) =>
        {
            if (c == "health")
                PlayerHealthController.Instance.health -= t;
            if (c == "energy")
                PlayerHealthController.Instance.energy -= t;

        });
        Help = new DebugCommand("Help", "Показывает все доступные команды", "Help", () => { showHelp = true; }); 
        ItemsID = new DebugCommand("ItemsID", "Создает файл с айди предметов и их описанием в \n \\AppData\\LocalLow\\QWERTY\\IRIDIUM HEART", "ItemsID", () => 
        {
            using (StreamWriter writer = new StreamWriter(File.Create(Application.persistentDataPath + "/ItemsID.txt")))
            {
                writer.Write("ID" + "-----" + "Name"+ "-----" + "ItemType" + "-----"+"MaxInSlot"+ "\n");
                foreach (InventoryItemInfo info in ItemBase.ItemsInfo.Values)
                {
                    writer.Write(info.id + "-----" + info.name + "-----" + info.itemType  +"-----" + info.maxItemsInInventortySlot + "\n");
                }
            }


        }); 



        commandList = new List<object>
        {
            AddItem,
            RemoveItem,
            AddHealth,
            AddEnergy,
            AddShard,
            Kill,
            SetActHeart,
            SetInvLevel,
            RemovePlayerVar,
            ItemsID,
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
            Rect viewport = new Rect(0, 0, Screen.width - 30, 160 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 220), scroll, viewport);
            for(int i = 0; i< commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.commandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(10, 130 * i, viewport.width - 100, 130);
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
                    else
                    {
                        (commandList[i] as DebugCommand<string, int>).Invoke(prop[1],Convert.ToInt32( prop[2]));
                    }
                }
                else if (commandList[i] as DebugCommand< int> != null)
                {
                    (commandList[i]as DebugCommand<int>).Invoke(Convert.ToInt32(prop[1]));
                }
                else if (commandList[i] as DebugCommand<int,int> != null)
                {
                    (commandList[i] as DebugCommand<int, int>).Invoke(Convert.ToInt32(prop[1]), Convert.ToInt32(prop[2]));
                }

                    inputString = "";
            }
        }
    }
}
