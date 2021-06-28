using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saves : MonoBehaviour
{
    public static bool test = false;
    public static SaveData Data = new SaveData();
    public static QuickSaveData QuickData = new QuickSaveData();
    public static GameConfig Config = new GameConfig();

    public static void DiscardLocalSaves()
    {
        QuickData = new QuickSaveData();
        PlayerStatus.lives = QuickData.lives;
        PlayerRings.energy = QuickData.energy;
        PlayerStatus.healingPotions = QuickData.healingPotions;
    }

    public static void Save()
    {
        Data.currentLocation = Locations.currentLocationNumber;
        Data.currentPosition = Player.instance.transform.position;
        string jsonSave = JsonUtility.ToJson(Data);
        File.WriteAllText(Application.dataPath + "data.json", jsonSave);
        QuickData.lives = PlayerStatus.lives;
        QuickData.healingPotions = PlayerStatus.healingPotions;
        QuickData.energy = PlayerRings.energy;
        jsonSave = JsonUtility.ToJson(QuickData);
        File.WriteAllText(Application.dataPath + "quick.json", jsonSave);
        SaveIcon.instance.Save();
    }

    public static void Load()
    {
        Data = new SaveData();
        string jsonLoad;
        try
        {
            jsonLoad = File.ReadAllText(Application.dataPath + "data.json");
            Data = JsonUtility.FromJson<SaveData>(jsonLoad);
        }
        catch (Exception e)
        {
            Data = new SaveData();
        }
        
        QuickData = new QuickSaveData();
        try
        {
            jsonLoad = File.ReadAllText(Application.dataPath + "quick.json");
            QuickData = JsonUtility.FromJson<QuickSaveData>(jsonLoad);
        }
        catch (FileNotFoundException)
        {
            QuickData = new QuickSaveData();
        }
        Locations.playerPosition = Data.currentPosition;
        Locations.LoadLocation(Data.currentLocation, -1, SaveType.None);
        PlayerStatus.lives = QuickData.lives;
        PlayerRings.energy = QuickData.energy;
        PlayerStatus.healingPotions = QuickData.healingPotions;
    }

    public static void SaveConfig()
    {
        string jsonSave = JsonUtility.ToJson(Config);
        File.WriteAllText(Application.dataPath + "config.json", jsonSave);
    }

    public static void LoadConfig()
    {
        try
        {
            string jsonLoad = File.ReadAllText(Application.dataPath + "config.json");
            Config = JsonUtility.FromJson<GameConfig>(jsonLoad);
        }
        catch (FileNotFoundException)
        {
            Config = new GameConfig();
        }
        Localization.language = Config.language;
    }

    public class SaveData
    {
        // Player state
        public int currentLocation;
        public Vector2 currentPosition;
        // World progress
        public List<string> mapSectorsOpen = new List<string>();
        public List<string> teleportNames = new List<string>();
        public List<TeleportPoint> teleportPointsOpen = new List<TeleportPoint>();
        public List<string> locksOpen = new List<string>();
        public List<string> dialoguesListened = new List<string>();
        public List<string> tutorialsComplete = new List<string>();
        public List<string> arenasCleared = new List<string>();
        public bool bossKilled;
        public Vector2 bossDeathPosition;
        // Loot
        public List<string> lootCollected = new List<string>();
        public List<Sprite> keys = new List<Sprite>();
        public List<Sprite> scrolls = new List<Sprite>();
        public List<RingInfo> rings = new List<RingInfo>();
        // Properties
        public List<Sprite> ringsActive = new List<Sprite>();
        public int sanity = 1 + BoolHelper.BoolToInt(test) * 99;
        public int sanityUsed;
        // Effects
        public bool map;
        public float knifeDistanceKoff = 1;
        public bool knives = test;
        public bool dash = test;
    }

    public class QuickSaveData
    {
        // Player state
        public int lives = PlayerStatus.maxLives;
        public int healingPotions;
        public int energy;
        // World
        public List<string> lootCollected = new List<string>();
        public List<string> combatAreasCleared = new List<string>();
        public List<string> checkpointsUsed = new List<string>();
    }

    public class GameConfig
    {
        public bool loaded;
        public int language;
        public float musicVolume = 0.5f;
        public float soundVolume = 0.5f;
    }
}
