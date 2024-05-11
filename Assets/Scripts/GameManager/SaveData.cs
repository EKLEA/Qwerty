using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using Unity.Mathematics;
using UnityEngine.Audio;

[System.Serializable]
public struct SaveData
{
    public static SaveData Instance;

    //map
    public HashSet<string> sceneNames;
    //checkPoint 
    public string checkPointSceneName;
    public Vector3 checkPointPos;
    //player
    public int playerHealth;
    public float playerEnergy;
    public bool isPlayerHasHeart;
    public Vector3 playerPos;
    public string lastScene;

    // level list
    public float tempAddHpS;
    public float tempAddEnS;
    public int levelTierS;
    public bool SideCastS;
    public bool DownCastS;
    public bool UPCastS;
    public bool canDashS;
    public bool canDoubleWallJumpS;

    // enemy
    //shade
    public Vector3 shadePos;
    public string sceneWithShade;
    public Quaternion shadeRot;

    public void Initialize()
    {
        if (!File.Exists(Application.persistentDataPath + "/save.checkPoint.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.checkPoint.data")))
            {
            }
        }
        if (!File.Exists(Application.persistentDataPath + "/save.player.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.player.data")))
            {
            }
        }

        if (!File.Exists(Application.persistentDataPath + "/save.pLevelList.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.pLevelList.data")))
            {
            }
        }
        if (!File.Exists(Application.persistentDataPath + "/save.inventory.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.inventory.data")))
            {
            }
        }
        if (!File.Exists(Application.persistentDataPath + "/save.shade.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.shade.data")))
            {
            }
        }
        if (!File.Exists(Application.persistentDataPath + "/save.settings.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(Application.persistentDataPath + "/save.settings.data")))
            {
            }
        }


        if (sceneNames == null)
        {
            sceneNames = new HashSet<string>();
        }
    }
    public void SaveSettings(AudioMixer audioMixer)
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.settings.data")))
        {
            
            writer.Write(Screen.fullScreen);
            float t;
            audioMixer.GetFloat("Volume", out t);
            writer.Write(t);
        }
    }
    bool fullscreen;
    public void LoadSettings(ref float volume)
    {
        if (File.Exists(Application.persistentDataPath + "/save.settings.data") && new FileInfo(Application.persistentDataPath + "/save.settings.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.settings.data")))
            {
                fullscreen= reader.ReadBoolean();
                volume = reader.ReadSingle();
                Screen.fullScreen = fullscreen;
            }

        }
        else
        {
            volume = 0;
            Screen.fullScreen = true;
        }
    }
    public void SaveCheckPoint()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.checkPoint.data")))
        {
            writer.Write(checkPointSceneName);
            writer.Write(checkPointPos.x);
            writer.Write(checkPointPos.y);
            writer.Write(checkPointPos.z);
        }
    }

    public void LoadCheckPoint()
    {
        if (File.Exists(Application.persistentDataPath + "/save.checkPoint.data") && new FileInfo(Application.persistentDataPath + "/save.checkPoint.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.checkPoint.data")))
            {
                checkPointSceneName = reader.ReadString();
                checkPointPos.x = reader.ReadSingle();
                checkPointPos.y = reader.ReadSingle();
                checkPointPos.z = reader.ReadSingle();
            }

        }
    }

    public void SavePlayerData()//помогите...
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.player.data")))
        {
            playerHealth = (int)PlayerController.Instance.playerHealthController.maxHealth;// сохранение базовых значений без предметов
            writer.Write(playerHealth);
            playerEnergy = PlayerController.Instance.playerHealthController.maxEnergy;// сохранение базовых значений без предметов
            writer.Write(playerEnergy);
            isPlayerHasHeart = PlayerController.Instance.playerHealthController.isHeartHas;
            writer.Write(isPlayerHasHeart);
            playerPos = PlayerController.Instance.transform.position;
            writer.Write(playerPos.x);
            writer.Write(playerPos.y);
            writer.Write(playerPos.z);
            lastScene = SceneManager.GetActiveScene().name;
            writer.Write(lastScene);

        }
    }
    public void LoadPlayerData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.player.data") && new FileInfo(Application.persistentDataPath + "/save.player.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.player.data")))
            {
                playerHealth = reader.ReadInt32();
                playerEnergy = reader.ReadSingle();
                isPlayerHasHeart = reader.ReadBoolean();
                playerPos.x = reader.ReadSingle();
                playerPos.y = reader.ReadSingle();
                playerPos.z = reader.ReadSingle();
                lastScene = reader.ReadString();

                SceneManager.LoadScene(lastScene);
                PlayerController.Instance.transform.position = playerPos;
                PlayerHealthController.Instance.isHeartHas = isPlayerHasHeart;
                PlayerHealthController.Instance.maxHealth = playerHealth;
                PlayerHealthController.Instance.maxEnergy = playerEnergy;
            }
        }
        else
        {
            Debug.Log("Файл не существует");
            PlayerController.Instance.playerHealthController.maxHealth = 5;
            PlayerController.Instance.playerHealthController.maxEnergy = 5;

            PlayerController.Instance.playerHealthController.isHeartHas = true;
        }
    }

    public void SavePlayerLevelListData()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.pLevelList.data")))
        {
            tempAddHpS = PlayerController.Instance.playerLevelList.tempAddHP;
            writer.Write(tempAddHpS);

            tempAddEnS = PlayerController.Instance.playerLevelList.tempAddEN;
            writer.Write(tempAddEnS);

            levelTierS = PlayerController.Instance.playerLevelList.levelTier;
            writer.Write(levelTierS);

            SideCastS = PlayerController.Instance.playerLevelList.SideCast;
            writer.Write(SideCastS);

            DownCastS = PlayerController.Instance.playerLevelList.DownCast;
            writer.Write(DownCastS);

            UPCastS = PlayerController.Instance.playerLevelList.UPCast;
            writer.Write(UPCastS);

            canDashS = PlayerController.Instance.playerLevelList.canDash;
            writer.Write(canDashS);

            canDoubleWallJumpS = PlayerController.Instance.playerLevelList.canDoubleWallJump;
            writer.Write(canDoubleWallJumpS);

        }
    }
    public void LoadPlayerLevelListData()
    {
        if (File.Exists(Application.persistentDataPath + "/save.pLevelList.data") && new FileInfo(Application.persistentDataPath + "/save.pLevelList.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.pLevelList.data")))
            {
                tempAddHpS = reader.ReadSingle();
                tempAddEnS = reader.ReadSingle();
                levelTierS = reader.ReadInt32();
                SideCastS = reader.ReadBoolean();
                DownCastS = reader.ReadBoolean();
                UPCastS = reader.ReadBoolean();
                canDashS = reader.ReadBoolean();
                canDoubleWallJumpS = reader.ReadBoolean();

                PlayerController.Instance.playerLevelList.tempAddHP = tempAddHpS;
                PlayerController.Instance.playerLevelList.tempAddEN = tempAddEnS;
                PlayerController.Instance.playerLevelList.levelTier = levelTierS;
                PlayerController.Instance.playerLevelList.SideCast = SideCastS;
                PlayerController.Instance.playerLevelList.DownCast = DownCastS;
                PlayerController.Instance.playerLevelList.UPCast = UPCastS;
                PlayerController.Instance.playerLevelList.canDash = canDashS;
                PlayerController.Instance.playerLevelList.canDoubleWallJump = canDoubleWallJumpS;
            }
        }
        else
        {
            Debug.Log("Файл не существует");
            PlayerController.Instance.playerLevelList.tempAddHP = 0;
            PlayerController.Instance.playerLevelList.tempAddEN = 0;
            PlayerController.Instance.playerLevelList.levelTier = 0;
            PlayerController.Instance.playerLevelList.SideCast = false;
            PlayerController.Instance.playerLevelList.DownCast = false;
            PlayerController.Instance.playerLevelList.UPCast = false;
            PlayerController.Instance.playerLevelList.canDash = false;
            PlayerController.Instance.playerLevelList.canDoubleWallJump = false;
        }
    }


    public void SavePlayerInv()
    {
        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.inventory.data")))
        {
            // equipped
            var slots = PlayerInventory.Instance.equippedItems.GetAllSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    writer.Write(slots[i].item.info.id);
                    writer.Write(slots[i].item.state.count);
                    writer.Write(slots[i].item.state.IsEquipped);
                }
                else
                {
                    writer.Write("Null");
                    writer.Write(0);
                    writer.Write(false);
                }
            }
            //weapon
            slots = PlayerInventory.Instance.weaponAndPerks.GetAllSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    writer.Write(slots[i].item.info.id);
                    writer.Write(slots[i].item.state.count);
                    writer.Write(slots[i].item.state.IsEquipped);
                }
                else
                {
                    writer.Write("Null");
                    writer.Write(0);
                    writer.Write(false);
                }
            }
            //storage
            slots = PlayerInventory.Instance.storageItems.GetAllSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    writer.Write(slots[i].item.info.id);
                    writer.Write(slots[i].item.state.count);
                    writer.Write(slots[i].item.state.IsEquipped);
                }
                else
                {
                    writer.Write("Null");
                    writer.Write(0);
                    writer.Write(false);
                }
            }
            //Collectables
            slots = PlayerInventory.Instance.collectableItems.GetAllSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    writer.Write(slots[i].item.info.id);
                    writer.Write(slots[i].item.state.count);
                    writer.Write(slots[i].item.state.IsEquipped);
                }
                else
                {
                    writer.Write("Null");
                    writer.Write(0);
                    writer.Write(false);
                }
            }
            //craftComponents
            slots = PlayerInventory.Instance.craftComponents.GetAllSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    writer.Write(slots[i].item.info.id);
                    writer.Write(slots[i].item.state.count);
                    writer.Write(slots[i].item.state.IsEquipped);
                }
                else
                {
                    writer.Write("Null");
                    writer.Write(0);
                    writer.Write(false);
                }
            }
        }
    }

    public void LoadPlayerInv()
    {
        if (File.Exists(Application.persistentDataPath + "/save.inventory.data") && new FileInfo(Application.persistentDataPath + "/save.inventory.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.inventory.data")))
            {
               
                string id;
                int countT;
                bool isEqut;
                Item item;
                //equipped
                var slots = PlayerInventory.Instance.equippedItems.GetAllSlots();
                for (int i = 0; i < slots.Length; i++)
                {
                    id = reader.ReadString();
                    countT = reader.ReadInt32();
                    isEqut = reader.ReadBoolean();
                    if (id == "Null")
                        continue;
                    else
                    {
                        item = new Item(ItemBase.ItemsInfo[id], countT);
                        item.state.IsEquipped = isEqut;
                        slots[i].item = item;
                    }
                }
                //weapon
                slots = PlayerInventory.Instance.weaponAndPerks.GetAllSlots();
                for (int i = 0; i < slots.Length; i++)
                {
                    id = reader.ReadString();
                    countT = reader.ReadInt32();
                    isEqut = reader.ReadBoolean();
                    if (id == "Null")
                        continue;
                    else
                    {
                        item = new Item(ItemBase.ItemsInfo[id], countT);
                        item.state.IsEquipped = isEqut;
                        slots[i].item = item;
                    }
                }
                //storage
                slots = PlayerInventory.Instance.storageItems.GetAllSlots();
                for (int i = 0; i < slots.Length; i++)
                {
                    id = reader.ReadString();
                    countT = reader.ReadInt32();
                    isEqut = reader.ReadBoolean();
                    if (id == "Null")
                        continue;
                    else
                    {
                        item = new Item(ItemBase.ItemsInfo[id], countT);
                        item.state.IsEquipped = isEqut;
                        slots[i].item = item;
                    }
                }
                //Collectables
                slots = PlayerInventory.Instance.collectableItems.GetAllSlots();
                for (int i = 0; i < slots.Length; i++)
                {
                    id = reader.ReadString();
                    countT = reader.ReadInt32();
                    isEqut = reader.ReadBoolean();
                    if (id == "Null")
                        continue;
                    else
                    {
                        item = new Item(ItemBase.ItemsInfo[id], countT);
                        item.state.IsEquipped = isEqut;
                        slots[i].item = item;
                    }
                }
                //craftComponents
                slots = PlayerInventory.Instance.craftComponents.GetAllSlots();
                for (int i = 0; i < slots.Length; i++)
                {
                    id = reader.ReadString();
                    countT = reader.ReadInt32();
                    isEqut = reader.ReadBoolean();
                    if (id == "Null")
                        continue;
                    else
                    {
                        item = new Item(ItemBase.ItemsInfo[id], countT);
                        item.state.IsEquipped = isEqut;
                        slots[i].item = item;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Файл не существует");
            PlayerInventory.Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["body0"], 1));
            PlayerInventory.Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["arm0"], 1));
            PlayerInventory.Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["legs0"], 1));
        }
    }

    public void SaveHeartEnemy()
    {
        if (File.Exists(Application.persistentDataPath + "/save.shade.data"))
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(Application.persistentDataPath + "/save.shade.data")))
            {
                sceneWithShade = SceneManager.GetActiveScene().name;
                shadePos = HeartEnemyLogic.Instance.transform.position;
                shadeRot = HeartEnemyLogic.Instance.transform.rotation;

                writer.Write(sceneWithShade);
                writer.Write(shadePos.x);
                writer.Write(shadePos.y);
                writer.Write(shadePos.z);

                writer.Write(shadeRot.x);
                writer.Write(shadeRot.y);
                writer.Write(shadeRot.z);
                writer.Write(shadeRot.w);
            }
        }
    }
    public void LoadHeartEnemy()
    {
        if (File.Exists(Application.persistentDataPath + "/save.shade.data") && new FileInfo(Application.persistentDataPath + "/save.shade.data").Length > 0)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(Application.persistentDataPath + "/save.shade.data")))
            {
                sceneWithShade = reader.ReadString();
                shadePos.x = reader.ReadSingle();
                shadePos.y = reader.ReadSingle();
                shadePos.z = reader.ReadSingle();
                float rotatioX = reader.ReadSingle();
                float rotatioY = reader.ReadSingle();
                float rotatioZ = reader.ReadSingle();
                float rotatioW = reader.ReadSingle();
                shadeRot = new Quaternion(rotatioX, rotatioY, rotatioZ, rotatioW);
            }
        }
        else
        {
            Debug.Log("Врага срдца нет");
        }
    }
}
